using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUpload.DataAccess.Models
{
    public class User : IdentityUser<int>
    {
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<FileDescription> FileDescriptions { get; set; }
    }
}
