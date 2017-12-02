using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIS560FinalProject.Migrations
{
    public partial class scrimsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Aliases_AliasID",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "Population",
                table: "Schools",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Winner",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TournamentID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "IGN",
                table: "Aliases",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Scrims",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Score1 = table.Column<int>(type: "int", nullable: true),
                    Score2 = table.Column<int>(type: "int", nullable: true),
                    Team1ID = table.Column<int>(type: "int", nullable: false),
                    Team2ID = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scrims", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Scrims_Teams_Team1ID",
                        column: x => x.Team1ID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scrims_Teams_Team2ID",
                        column: x => x.Team2ID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentID",
                table: "Matches",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Scrims_Team1ID",
                table: "Scrims",
                column: "Team1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Scrims_Team2ID",
                table: "Scrims",
                column: "Team2ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentID",
                table: "Matches",
                column: "TournamentID",
                principalTable: "Tournaments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Aliases_AliasID",
                table: "Members",
                column: "AliasID",
                principalTable: "Aliases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Aliases_AliasID",
                table: "Members");

            migrationBuilder.DropTable(
                name: "Scrims");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TournamentID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TournamentID",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "Population",
                table: "Schools",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Winner",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IGN",
                table: "Aliases",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Aliases_AliasID",
                table: "Members",
                column: "AliasID",
                principalTable: "Aliases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
