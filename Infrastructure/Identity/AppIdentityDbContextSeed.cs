using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Haripriya",
                    Email = "Haripriya@gmail.com",
                    UserName = "Haripriya@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Haripriy",
                        LastName = "Kamalakannan",
                        Street = "Test street",
                        City = "India",
                        State = "TN",
                        Zipcode = "90210"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}