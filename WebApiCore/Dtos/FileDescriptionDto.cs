using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Dtos
{
    public class FileDescriptionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public FileDescriptionDto()
        {
            CreatedTimestamp = DateTime.Now;
        }
    }
}
