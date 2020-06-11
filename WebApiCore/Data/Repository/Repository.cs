using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.DataAccess.Data;
using WebApiCore.Models;

namespace WebApiCore.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _applicationDbContext.Add<T>(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : class
        {
            //set command timeout if having heavy data to import
            _applicationDbContext.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
            _applicationDbContext.AddRange(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _applicationDbContext.Remove<T>(entity);
        }

        public async Task<IEnumerable<FileDescription>> GetAllUserFileDescription(int userId)
        {
            return await _applicationDbContext.FileDescriptions.Where(u => u.UserId == userId).ToListAsync();
        }

        public Task<FileDescription> GetFileDescription(int id)
        {
            return _applicationDbContext.FileDescriptions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ImportFileDescription>> GetAllUserImportedFileDescription(int userId)
        {
            return await _applicationDbContext.ImportFileDescriptions.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<ImportFileDescription> GetImportedFileDescription(int id)
        {
            return await _applicationDbContext.ImportFileDescriptions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(d => d.Id == userId);
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
