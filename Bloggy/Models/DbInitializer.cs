using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bloggy.Models
{
    public class DbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new IdentityRole("User")).Wait();
                roleManager.CreateAsync(new IdentityRole("Administrator")).Wait();
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    Email = "chrisporter@testmail.com",
                    NormalizedEmail = "CHRISPORTER@TESTMAIL.COM",
                    UserName = "chrisporter@testmail.com",
                    NormalizedUserName = "CHRISPORTER@TESTMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    AuthorAlias = "chrisporter",
                    SecurityStamp = Guid.NewGuid().ToString("D") //to track important profile updates (e.g. password change)
                };

                //Add to store
                IdentityResult result = userManager.CreateAsync(user, "Bl0ggyRules!").Result;
                if (result.Succeeded)
                {
                    //Add to role
                    userManager.AddToRoleAsync(user, "User").Wait();
                }

                var admin = new ApplicationUser()
                {
                    Email = "paularquiergedda@testmail.com",
                    NormalizedEmail = "PAULARQUIERGEDDA@TESTMAIL.COM",
                    UserName = "paularquiergedda@testmail.com",
                    NormalizedUserName = "PAULARQUIERGEDDA@TESTMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    AuthorAlias = "paularquiergedda",
                    SecurityStamp = Guid.NewGuid().ToString("D") //to track important profile updates (e.g. password change)
                };

                //Add to store
                result = userManager.CreateAsync(admin, "Bl0ggyRules!").Result;
                if (result.Succeeded)
                {
                    //Add to role
                    userManager.AddToRoleAsync(admin, "Administrator").Wait();
                }
                
            }
        }

        public static void SeedData(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.AddRange
                (
                    new Category()
                    {
                        Name = "Unsafe Act"
                    },
                    new Category ()
                    {
                        Name = "Condition"
                    },
                    new Category()
                    {
                        Name = "Equipment or Structure"
                    }
                );
                context.SaveChanges();
            }

            if (!context.BlogPosts.Any())
            {
                //Grabbing first one
                var user = userManager.GetUsersInRoleAsync("User").Result.FirstOrDefault();

                context.AddRange
                (
                    new BlogPost()
                    {
                        Title = "Toilet light",
                        Content = "the light bulb in the toilet exploded. Many pieces of glass were found on the ground",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        DiscoveryDate = DateTime.UtcNow.AddMinutes(-600),
                        ImageUrl = "/images/test1.jpg",
                        UpVotes = 0,
                        CategoryId = 1,
                        Status = "Open",
                        UserId = user.Id,
                        Location = "Toilet",


                    },
                    new BlogPost()
                    {
                        Title = "Chimic reaction problem",
                        Content = "Chemical reaction in a laboratory following an experiment during a UoM course. No injuries but material damage was reported",
                        CreatedDate = DateTime.UtcNow.AddDays(-1),
                        UpdatedDate = DateTime.UtcNow.AddDays(-1),
                        DiscoveryDate = DateTime.UtcNow.AddDays(-1).AddMinutes(-300),
                        ImageUrl = "/images/test2.jpg",
                        CategoryId = 2,
                        UpVotes = 0,
                        Status = "Open",
                        UserId = user.Id,
                        Location = "Laboratory"

                    },
                    new BlogPost()
                    {
                        Title = "Door of the 122 room",
                        Content = "The door to room 122 was found with the handle broken off. This happened during the night no clue as to how this problem arose.",
                        CreatedDate = DateTime.UtcNow.AddDays(-2),
                        UpdatedDate = DateTime.UtcNow.AddDays(-2),
                        DiscoveryDate = DateTime.UtcNow.AddDays(-2).AddMinutes(-1000),
                        ImageUrl = "/images/test3.jpg",
                        CategoryId = 2,
                        UpVotes = 0,
                        Status = "Open",
                        UserId = user.Id,
                        Location = "Room 122"

                    }
                );
                context.SaveChanges();
            }

            if (!context.Investigations.Any())
            {
                //Grabbing first one
                var user = userManager.GetUsersInRoleAsync("Administrator").Result.FirstOrDefault();

                context.AddRange
                (
                    new Investigation()
                    {
                        Title = "Investigation 1",
                        Content = "The bulb was too old, the administration called the electrician to change the bulb and fix the problem",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        ImageUrl = "/images/test1.jpg",
                        UserId = user.Id,
                    },
                    new Investigation()
                    {
                        Title = "Investigation 2",
                        Content = "We are currently analysing the different products used in this experiment. We do not yet know what the problem is",
                        CreatedDate = DateTime.UtcNow.AddDays(-1),
                        UpdatedDate = DateTime.UtcNow.AddDays(-1),
                        ImageUrl = "/images/test2.jpg",
                        UserId = user.Id,

                    }
                );
                context.SaveChanges();
            }
        }
    }

}
