using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Dtos
{
    public class FileDescriptionForResultDto
    {
            public int Id { get; set; }
            public string Description { get; set; }
            public string ContentType { get; set; }
            public string Name { get; set; }
    }
}
