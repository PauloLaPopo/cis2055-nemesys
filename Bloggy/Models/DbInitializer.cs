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
                    Email = "testuser@testmail.com",
                    NormalizedEmail = "TESTUSER@TESTMAIL.COM",
                    UserName = "testuser@testmail.com",
                    NormalizedUserName = "TESTUSER@TESTMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    AuthorAlias = "testuser",
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
                    Email = "testadmin@testmail.com",
                    NormalizedEmail = "TESTADMIN@TESTMAIL.COM",
                    UserName = "testadmin@testmail.com",
                    NormalizedUserName = "TESTADMIN@TESTMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    AuthorAlias = "testadmin",
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
                        Name = "Uncategorised"
                    },
                    new Category ()
                    {
                        Name = "Chimic Danger"
                    },
                    new Category()
                    {
                        Name = "Technical Danger"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Statues.Any())
            {
                context.AddRange
                (
                    new Status()
                    {
                        Id = 1,
                        Name = "Open"
                    },
                    new Status()
                    {
                        Id = 2,
                        Name = "Closed"
                    },
                    new Status()
                    {
                        Id = 3,
                        Name = "Being investigated"
                    },
                    new Status()
                    {
                        Id = 4,
                        Name = "No action required"
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
                        Title = "Connard",
                        Content = "Today's AGA is characterized by a series of discussions and debates around ...",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        DiscoveryDate = DateTime.UtcNow.AddMinutes(-600),
                        ImageUrl = "/images/seed1.jpg",
                        UpVotes = 0,
                        CategoryId = 1,
                        StatusId = 1,
                        UserId = user.Id,
                        Location = "Toilet",


                    },
                    new BlogPost()
                    {
                        Title = "Traffic is incredible",
                        Content = "Today's traffic can't be described using words. Only an image can do that ...",
                        CreatedDate = DateTime.UtcNow.AddDays(-1),
                        UpdatedDate = DateTime.UtcNow.AddDays(-1),
                        DiscoveryDate = DateTime.UtcNow.AddDays(-1).AddMinutes(-300),
                        ImageUrl = "/images/seed2.jpg",
                        CategoryId = 2,
                        UpVotes = 0,
                        StatusId = 1,
                        UserId = user.Id,
                        Location = "Laboratory"

                    },
                    new BlogPost()
                    {
                        Title = "Pute",
                        Content = "Clouds clouds all around us. I thought spring started already, but ...",
                        CreatedDate = DateTime.UtcNow.AddDays(-2),
                        UpdatedDate = DateTime.UtcNow.AddDays(-2),
                        DiscoveryDate = DateTime.UtcNow.AddDays(-2).AddMinutes(-1000),
                        ImageUrl = "/images/seed3.jpg",
                        CategoryId = 2,
                        UpVotes = 0,
                        StatusId = 1,
                        UserId = user.Id,
                        Location = "Room 122"

                    }
                );
                context.SaveChanges();
            }
        }
    }

}
