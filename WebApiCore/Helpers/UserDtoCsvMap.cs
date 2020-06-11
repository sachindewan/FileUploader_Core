using CsvHelper.Configuration;
using WebApiCore.Models;

namespace WebApiCore.Helpers
{
    public sealed class UserDtoCsvMap : ClassMap<AnonymousUser>
    {
        public UserDtoCsvMap()
        {
            Map(x => x.City).Name("City");
            Map(x => x.UserName).Name("Username");
            Map(x => x.Gender).Name("Gender");
            Map(x => x.DateOfBirth).Name("DateOfBirth");
            Map(x => x.KnownAs).Name("KnownAs");
            Map(x => x.Country).Name("Country");
            Map(x => x.City).Name("City");
        }
    }
}
