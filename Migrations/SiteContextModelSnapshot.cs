﻿// <auto-generated />
using CIS_560_Final_Project.Entities;
using CIS_560_Final_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace CIS560FinalProject.Migrations
{
    [DbContext(typeof(SiteContext))]
    partial class SiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("CIS_560_Final_Project.Models.Aliases", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IGN")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("MembersID");

                    b.HasKey("ID");

                    b.HasIndex("MembersID");

                    b.ToTable("Aliases");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Games", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abv")
                        .HasMaxLength(8);

                    b.Property<string>("Company")
                        .HasMaxLength(50);

                    b.Property<string>("name")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Matches", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datetime");

                    b.Property<int>("MatchNumber");

                    b.Property<int>("Team1ID");

                    b.Property<int>("Team2ID");

                    b.Property<int>("TournamentID");

                    b.Property<int?>("Winner");

                    b.HasKey("ID");

                    b.HasIndex("Team1ID");

                    b.HasIndex("Team2ID");

                    b.HasIndex("TournamentID");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Members", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("Joined");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Members");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Members");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Schools", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Address2")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("Population");

                    b.Property<int>("State");

                    b.HasKey("ID");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Scrims", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datetime");

                    b.Property<int?>("Score1");

                    b.Property<int?>("Score2");

                    b.Property<int>("Team1ID");

                    b.Property<int>("Team2ID");

                    b.Property<int?>("Winner");

                    b.HasKey("ID");

                    b.HasIndex("Team1ID");

                    b.HasIndex("Team2ID");

                    b.ToTable("Scrims");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Teams", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SchoolID");

                    b.HasKey("ID");

                    b.HasIndex("GameID");

                    b.HasIndex("SchoolID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsMembers", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlayersID");

                    b.Property<int>("TeamsID");

                    b.HasKey("ID");

                    b.HasIndex("PlayersID");

                    b.HasIndex("TeamsID");

                    b.ToTable("TeamsMembers");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsTournaments", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TeamID");

                    b.Property<int>("TournamentID");

                    b.HasKey("ID");

                    b.HasIndex("TeamID");

                    b.HasIndex("TournamentID");

                    b.ToTable("TeamsTournaments");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Tournaments", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("GamesID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("ID");

                    b.HasIndex("GamesID");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Users", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(127);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(127);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(127);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(127);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(127);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(127);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(127);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(127);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(127);

                    b.Property<string>("RoleId")
                        .HasMaxLength(127);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(127);

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(127);

                    b.Property<string>("Name")
                        .HasMaxLength(127);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Coaches", b =>
                {
                    b.HasBaseType("CIS_560_Final_Project.Models.Members");

                    b.Property<bool>("IsManager");

                    b.Property<int>("YearsCoaching");

                    b.ToTable("Coaches");

                    b.HasDiscriminator().HasValue("Coaches");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Players", b =>
                {
                    b.HasBaseType("CIS_560_Final_Project.Models.Members");

                    b.Property<int>("AliasID");

                    b.Property<int>("Year");

                    b.HasIndex("AliasID");

                    b.ToTable("Players");

                    b.HasDiscriminator().HasValue("Players");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsCoached", b =>
                {
                    b.HasBaseType("CIS_560_Final_Project.Models.Members");

                    b.Property<int>("CoachID");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TeamID");

                    b.Property<bool>("WasManager");

                    b.HasIndex("CoachID");

                    b.HasIndex("TeamID");

                    b.ToTable("TeamsCoached");

                    b.HasDiscriminator().HasValue("TeamsCoached");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsPlayed", b =>
                {
                    b.HasBaseType("CIS_560_Final_Project.Models.Members");

                    b.Property<DateTime>("EndDate")
                        .HasColumnName("TeamsPlayed_EndDate");

                    b.Property<int>("PlayerID");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("TeamsPlayed_StartDate");

                    b.Property<int>("TeamID")
                        .HasColumnName("TeamsPlayed_TeamID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("TeamsPlayed");

                    b.HasDiscriminator().HasValue("TeamsPlayed");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Aliases", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Members", "Member")
                        .WithMany()
                        .HasForeignKey("MembersID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Matches", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Tournaments", "Tournament")
                        .WithMany()
                        .HasForeignKey("TournamentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Members", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Scrims", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Teams", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Games", "Game")
                        .WithMany()
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Schools", "School")
                        .WithMany()
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsMembers", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Players", "Player")
                        .WithMany("TeamsMembers")
                        .HasForeignKey("PlayersID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsTournaments", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Tournaments", "Tournament")
                        .WithMany()
                        .HasForeignKey("TournamentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Tournaments", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Games", "Game")
                        .WithMany()
                        .HasForeignKey("GamesID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.Players", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Aliases", "Alias")
                        .WithMany()
                        .HasForeignKey("AliasID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsCoached", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Coaches", "Coach")
                        .WithMany("TeamsCoached")
                        .HasForeignKey("CoachID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIS_560_Final_Project.Models.TeamsPlayed", b =>
                {
                    b.HasOne("CIS_560_Final_Project.Models.Players", "Players")
                        .WithMany()
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIS_560_Final_Project.Models.Teams", "Teams")
                        .WithMany()
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
