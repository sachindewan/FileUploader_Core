using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Helpers;
using WebApiCore.Models;

namespace WebApiCore.Data.Initializer
{
    public class Seed
    {
        public static void SeedData(UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                var roles = new List<Role>()
                {
                    new Role(){Name = SD.Admin},
                    new Role(){ Name =SD.Member}
                };
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
                foreach (var user in users)
                {
                    var result = userManager.CreateAsync(user, "Password").GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user,SD.Member).GetAwaiter().GetResult();
                    }

                }
                //lets create admin user
                var adminUser = new User()
                {
                    UserName = "Admin",
                };
                 var resultData = userManager.CreateAsync(adminUser, "Admin@123").GetAwaiter().GetResult();
                if (resultData.Succeeded)
                {
                    var admin = userManager.FindByNameAsync(adminUser.UserName).GetAwaiter().GetResult();
                    userManager.AddToRoleAsync(admin, SD.Admin).GetAwaiter().GetResult();
                }
            }
        }
    }
}
