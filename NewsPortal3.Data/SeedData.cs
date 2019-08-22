using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; 
using NewsPortal3.Models.ViewModels;
using NewsPortal3.Data.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models;
using NewsPortal3.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace NewsPortal3.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            //using (var context = new DataContext(
            //   serviceProvider.GetRequiredService<DbContextOptions<DataContext>>())
            //)
            using (var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>())
            {

                string[] roleNames = Constants.Roles.All;
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new Role(roleName));
                    }
                }
            }
           
            using (var userManager = serviceProvider.GetRequiredService<UserManager<User>>())
            {
                // Look for any users.
                if (userManager.Users.Any())
                {
                    return;   // DB has been seeded
                }
                var user = new User
                {
                    Email = "gvg_ravil@mail.ru",
                    UserName = "rav3L",
                    LastName = "Gabdullin",
                    FirstName = "Ravil",
                    MiddleName = "Vasilevich",
                    BirthDate = new DateTime(1998, 2, 20)
                };
                await userManager.CreateAsync(user, "1Ravil*");
                await userManager.AddToRoleAsync(user, Constants.Roles.Administrator);
                user = new User
                {
                    Email = "gvg.ravil@gmail.com",
                    UserName = "nagebator",
                    LastName = "Surname",
                    FirstName = "Name",
                    MiddleName = "Patronymich",
                    BirthDate = new DateTime(2000, 1, 31)
                };
                await userManager.CreateAsync(user, "1Nagib*");
                await userManager.AddToRoleAsync(user, Constants.Roles.Editor);
            }
        }
    }
}
