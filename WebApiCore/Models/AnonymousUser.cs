using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class AnonymousUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime Created { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual ImportFileDescription  ImportFileDescription { get; set; }
        public AnonymousUser()
        {
            Created = DateTime.Now;
        }
       
    }
}
