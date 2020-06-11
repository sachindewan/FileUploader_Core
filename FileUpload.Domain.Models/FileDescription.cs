using System;
using System.Collections.Generic;

namespace FileUpload.Domain.Models
{
    public class FileDescription
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string ContentType { get; set; }
    }
}
