using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class ImportFileDescription
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string ContentType { get; set; }
        public virtual ICollection<AnonymousUser> AnonymousUsers { get; set; }

    }
}
