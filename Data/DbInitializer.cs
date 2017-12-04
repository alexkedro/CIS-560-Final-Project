using CIS_560_Final_Project.Models;
using System;
using System.Linq;
using CIS_560_Final_Project.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CIS_560_Final_Project.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SiteContext context)
        {
            context.Database.EnsureCreated();

            if(context.Games.Any())
            {
                return;
            }

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


            if(context.Schools.Any())
            {
                return;
            }
            var schools = new Schools[]
            {
                new Schools{Name="Kansas State University",Address1="Manhattan, KS",City="Manhattan",State=States.KS},
                new Schools{Name="University of Kansas",Address1="Lawerence, KS",City="Lawerence",State=States.KS}
            };

            foreach(Schools s in schools)
            {
                context.Schools.Add(s);
            }
            context.SaveChanges();

            if(context.Teams.Any())
            {
                return;
            }

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









            // Add roles
            string[] roles = new string[] {"Administrator", "User"};
            foreach(string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            // Add admin user
            if(context.Users.Any())
            {
                return;
            }
            var user = new Users
            {
                Email="admin@admin.com",
                NormalizedEmail="ADMIN@ADMIN.COM",
                UserName="Admin",
                NormalizedUserName="ADMIN",
                EmailConfirmed = true,
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<Users>();
                var hashed = password.HashPassword(user,"Password123");
                user.PasswordHash = hashed;

                var userStore = new UserStore<Users>(context);
                var result = userStore.CreateAsync(user);
            }
            context.SaveChanges();
        }

    }

}