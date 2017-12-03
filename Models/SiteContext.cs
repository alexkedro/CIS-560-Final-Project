using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CIS_560_Final_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CIS_560_Final_Project.Models
{
    public class SiteContext : IdentityDbContext<Users>
    {
        public DbSet<Schools> Schools { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<Coaches> Coaches { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Aliases> Aliases { get; set; }

        public DbSet<Tournaments> Tournaments { get; set; }
        public DbSet<TeamsCoached> TeamsCoached { get; set; }
        public DbSet<TeamsPlayed> TeamsPlayed { get; set; }
        public DbSet<TeamsMembers> TeamsMembers { get; set; }

        public DbSet<TeamsTournaments> TeamsTournaments { get; set; }
        public DbSet<Matches> Matches { get; set; }
        public DbSet<Scrims> Scrims { get; set; }
        
        public SiteContext (DbContextOptions<SiteContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            // Shorten key length for Identity 
            modelBuilder.Entity<Users>(entity => { 
            entity.Property(m => m.Email).HasMaxLength(127); 
            entity.Property(m => m.NormalizedEmail).HasMaxLength(127); 
            entity.Property(m => m.NormalizedUserName).HasMaxLength(127); 
            entity.Property(m => m.UserName).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityRole>(entity => { 
                entity.Property(m => m.Name).HasMaxLength(127); 
                entity.Property(m => m.NormalizedName).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => 
            { 
                entity.Property(m => m.LoginProvider).HasMaxLength(127); 
                entity.Property(m => m.ProviderKey).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityUserRole<string>>(entity => 
            { 
                entity.Property(m => m.UserId).HasMaxLength(127); 
                entity.Property(m => m.RoleId).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityUserToken<string>>(entity => 
            { 
                entity.Property(m => m.UserId).HasMaxLength(127); 
                entity.Property(m => m.LoginProvider).HasMaxLength(127); 
                entity.Property(m => m.Name).HasMaxLength(127); 
            
            }); 
        }
    }
}

