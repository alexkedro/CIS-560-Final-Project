using CIS_560_Final_Project.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using CIS_560_Final_Project.Entities;
using Microsoft.AspNetCore.Identity;

namespace CIS_560_Final_Project.Data
{
    public class DbInitializer 
    {

        // Seed database
        public static async Task Initialize(SiteContext context, UserManager<Users> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

           
            if(!context.Games.Any())
            {
                var games = new Games[]
                {
                    new Games{name="League of Legends", Abv="LoL", Company="RiotGames"},
                    new Games{name="Counter Strike Global Offensive", Abv="CSGO", Company="Valve"},
                    new Games{name="Dota 2", Abv="Dota", Company="Valve"},
                    new Games{name="Heros of the Storm", Abv="HOTS", Company="Blizzard"},
                    new Games{name="Heros of the Storm", Abv="HOTS", Company="Blizzard"},
                    new Games{name="Rocket League", Abv="RL", Company="Psyonix"}
                };

                foreach(Games g in games)
                {
                    context.Games.Add(g);
                }
                context.SaveChanges();
            }

            if(!context.Schools.Any())
            {
                var schools = new Schools[]
                {
                    new Schools{Name="Kansas State University",Address1="Manhattan, KS 66506",City="Manhattan",State=States.KS, Population = 24766},
                    new Schools{Name="University of Kansas",Address1="Lawerence, KS 66045",City="Lawerence",State=States.KS, Population = 28401}
                };

                foreach(Schools s in schools)
                {
                    context.Schools.Add(s);
                }
                context.SaveChanges();
            }

            if(!context.Teams.Any())
            {
                var teams = new Teams[]
                {
                    new Teams
                    {
                        Name="KSU Team",
                        Game=context.Games.Single(s=>s.name=="League of Legends"),
                        School=context.Schools.Single(s=>s.Name=="Kansas State University")
                    },
                    new Teams
                    {
                        Name="KSU Team",
                        Game=context.Games.Single(s=>s.name=="League of Legends"),
                        School=context.Schools.Single(s=>s.Name=="University of Kansas")
                    }

                };
                foreach(Teams t in teams)
                {
                    context.Teams.Add(t);
                }
                context.SaveChanges();
            }

            if (!context.Tournaments.Any())
            {
                var tournaments = new Tournaments[]
                {
                    new Tournaments
                    {
                        Name="Midwest Regionals",
                        StartDate = new DateTime(2017, 12, 8),
                        EndDate = new DateTime(2017, 12, 10),
                        Game=context.Games.Single(s=>s.name=="League of Legends")
                    }

                };
                foreach (Tournaments t in tournaments)
                {
                    context.Tournaments.Add(t);
                }
                context.SaveChanges();
            }

            if (!context.Matches.Any())
            {
                var matches = new Matches[]
                {
                    new Matches
                    {
                        MatchNumber = 1,
                        Team1=context.Teams.Single(t => t.ID == 1),
                        Team2=context.Teams.Single(t => t.ID == 2),
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 10),
                        Tournament = context.Tournaments.Single(t => t.ID == 1)
                    },

                    new Matches
                    {
                        MatchNumber = 2,
                        Team1=context.Teams.Single(t => t.ID == 1),
                        Team2=context.Teams.Single(t => t.ID == 2),
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 10),
                        Tournament = context.Tournaments.Single(t => t.ID == 1)
                    },

                    new Matches
                    {
                        MatchNumber = 3,
                        Team1=context.Teams.Single(t => t.ID == 1),
                        Team2=context.Teams.Single(t => t.ID == 2),
                        Winner = 2,
                        Datetime = new DateTime(2017, 12, 10),
                        Tournament = context.Tournaments.Single(t => t.ID == 1)
                    },

                    new Matches
                    {
                        MatchNumber = 4,
                        Team1=context.Teams.Single(t => t.ID == 1),
                        Team2=context.Teams.Single(t => t.ID == 2),
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 10),
                        Tournament = context.Tournaments.Single(t => t.ID == 1)
                    }

                };

                foreach (Matches m in matches)
                {
                    context.Matches.Add(m);
                }
                context.SaveChanges();
            }

            //Create admin user and role
            await CreateAdmin(context, userManager, roleManager);
       }

        public static async Task CreateAdmin(SiteContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Add roles
            string[] roles = new string[] {"Admin", "User"};
            foreach(string role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Add admin user
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var user = new Users
                {
                    Email="admin@admin.com",
                    NormalizedEmail="ADMIN@ADMIN.COM",
                    UserName="admin@admin.com",
                    NormalizedUserName="ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, "Password123");

                var adminuser = await userManager.FindByEmailAsync("admin@admin.com");
                if(result.Succeeded)
                {
                    var addresult = await userManager.AddToRoleAsync(adminuser, "Admin");
                }
            }

            
        }

    }

}