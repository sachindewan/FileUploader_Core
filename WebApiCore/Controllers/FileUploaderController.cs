using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApiCore.Data.Repository;
using WebApiCore.DataAccess.Data;
using WebApiCore.Dtos;
using WebApiCore.Helpers;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    [ServiceFilter(typeof(AutherizeCurrentUserFilter))]
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IOptions<ApplicationConfiguration> _options;
        private readonly IMapper _mapper;

        public FileUploaderController(IRepository repository, IOptions<ApplicationConfiguration> options, IMapper mapper)
        {
            this._repository = repository;
            this._options = options;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetFileDesc")]
        public async Task<IActionResult> GetFileDescription(int id)
        {
            var filedesFromRepo = await _repository.GetFileDescription(id);

            var fileDescToReturn = _mapper.Map<FileDescriptionForResultDto>(filedesFromRepo);
            return Ok(fileDescToReturn);
        }

        [HttpGet("{id}", Name = "GetImportedFile")]
        public async Task<IActionResult> GetImportedFileDescription(int id)
        {
            var filedesFromRepo = await _repository.GetFileDescription(id);

            var fileDescToReturn = _mapper.Map<FileDescriptionForResultDto>(filedesFromRepo);
            return Ok(fileDescToReturn);
        }

        [HttpGet("getallimportedfile")]
        public async Task<IActionResult> GetAllUsersImportedFileDescription(int userId)
        {
            var filedesFromRepo = await _repository.GetAllUserImportedFileDescription(userId);

            var fileDescToReturn = _mapper.Map<IEnumerable<FileDescriptionForResultDto>>(filedesFromRepo);
            return Ok(fileDescToReturn);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllUsersFileDescription(int userId)
        {
            var filedesFromRepo = await _repository.GetAllUserFileDescription(userId);

            var fileDescToReturn = _mapper.Map<IEnumerable<FileDescriptionForResultDto>>(filedesFromRepo);
            return Ok(fileDescToReturn);
        }


        [HttpDelete("deleteuserfile/{fileId}")]
        public async Task<IActionResult> DeleteFileAsync(int userId,int fileId)
        {
            var filedesFromRepo = await _repository.GetFileDescription(fileId);
            if (filedesFromRepo == null) return NotFound();
            if (System.IO.File.Exists(Path.Combine(_options.Value.ServerUploadFolder, filedesFromRepo.FileName))){
                System.IO.File.Delete(Path.Combine(_options.Value.ServerUploadFolder, filedesFromRepo.FileName));
            }
            _repository.Delete<FileDescription>(filedesFromRepo);
            if (await _repository.SaveAll())
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("uploadfile")]
        //file lenth set to 200mb
        [RequestFormLimits(MultipartBodyLengthLimit = SD.MaxFileSize)]
        [RequestSizeLimit(SD.MaxFileSize)]
        [ServiceFilter(typeof(FileValidationFilter))]
        public async Task<IActionResult> UploadFiles(int userId, [FromForm] FileDescriptionDto fileDescriptionDto)
        {
            var file = fileDescriptionDto?.File;
            bool isTranffered = false;
            if (file?.Length > 0)
            {
                try
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');

                    fileDescriptionDto.FileName = fileDescriptionDto.Description = fileName;
                    fileDescriptionDto.ContentType = file.ContentType;
                    isTranffered = await file.SaveAsAsync(Path.Combine(_options.Value.ServerUploadFolder, fileName));
                }
                catch (Exception ex) when (ex.InnerException is TimeoutException)
                {
                    // [To Do] implement retry mechanism if fails to send the stream
                    //file.RetrySaveAsync(SD.Retry)
                }
                // getting User 
                if (isTranffered)
                {
                    var userFromRepo = await _repository.GetUser(userId);
                    if (userFromRepo == null)
                    {
                        return BadRequest();
                    }

                    var fileDesc = _mapper.Map<FileDescription>(fileDescriptionDto);

                    userFromRepo.FileDescriptions.Add(fileDesc);

                    if (await _repository.SaveAll())
                    {
                        var fileForRerurn = _mapper.Map<FileDescriptionForResultDto>(fileDesc);
                        return CreatedAtRoute("GetFileDesc", new { userId = fileDesc.UserId, id = fileDesc.Id }, fileForRerurn);
                    }
                }

            }


            return BadRequest("failed while saving the file");
        }

        [HttpPost("importfile")]
        [ServiceFilter(typeof(FileValidationFilter))]
        [RequestFormLimits(MultipartBodyLengthLimit = SD.MaxFileSize)]
        [RequestSizeLimit(SD.MaxFileSize)]
        public async Task<ActionResult> UploadCsv(int userId, [FromForm] FileDescriptionDto fileDescriptionDto)
        {
            List<AnonymousUser> anonymousUsers = new List<AnonymousUser>();
            var serviceDbContext = HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
            long fileSize = 0;
            using(var transaction = serviceDbContext.Database.BeginTransaction())
            {
                try
                {
                    using (var fileStream = fileDescriptionDto.File.OpenReadStream())
                    using (var reader = new StreamReader(fileStream, Encoding.Default))
                    using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                    {
                        //getting filename and contentType
                        var fileName = ContentDispositionHeaderValue.Parse(fileDescriptionDto.File?.ContentDisposition).FileName.ToString().Trim('"');
                        fileDescriptionDto.FileName = fileDescriptionDto.Description = fileName;
                        fileDescriptionDto.ContentType = fileDescriptionDto.File?.ContentType;
                        fileSize = fileDescriptionDto.File.Length;
                        //mapping csv file to class table schema
                        csv.Configuration.RegisterClassMap<UserDtoCsvMap>();
                        var records = csv.GetRecords<AnonymousUser>().ToList();

                        //adding import file 
                        var userFromRepo = await _repository.GetUser(userId);
                        var importFileMap = _mapper.Map<ImportFileDescription>(fileDescriptionDto);
                        userFromRepo.ImportFileDescriptions.Add(importFileMap);

                        if (await _repository.SaveAll())
                        {
                            //import file processing...
                            var importedFiledescFromRepo = await _repository.GetImportedFileDescription(importFileMap.Id);
                            foreach (var record in records)
                            {
                                record.ImportFileDescription = importedFiledescFromRepo;
                            }
                            anonymousUsers = records;
                            _repository.AddFileRange<AnonymousUser>(records, fileSize);
                            if (await _repository.SaveAll())
                            {
                                var fileForReturn = _mapper.Map<FileDescriptionForResultDto>(importedFiledescFromRepo);
                                await transaction.CommitAsync();
                                return CreatedAtRoute("GetFileDesc", new { userId = importedFiledescFromRepo.UserId, id = importedFiledescFromRepo.Id }, fileForReturn);

                            }
                        }

                    }
                }
                catch (Exception ex) when (ex.Message.Equals("The operation has timed out."))
                {
                    // [To Do]implement retry mechanism if fails to send the stream
                    var retryResult = await RetryHelper.RetryOnExceptionAsync(SD.Retry, TimeSpan.FromSeconds(10), async () =>
                    {
                        return await SaveRetry(anonymousUsers);
                    });
                    if (retryResult) {
                        await transaction.CommitAsync();
                        return Ok("data has been imported via retry mechanism");
                    }

                    await transaction.RollbackAsync();
                    return BadRequest(ex.Message);
                }
                await transaction.RollbackAsync();
            }
            
            return BadRequest("Error occured while dumping the data into data base");
        }

        private async Task<bool> SaveRetry(List<AnonymousUser> anonymousUsers)
        {

            _repository.AddRange<AnonymousUser>(anonymousUsers);
            if (await _repository.SaveAll())
            {
                return true;
            }
            return false;
        }

    }
}
