using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIS560FinalProject.Migrations
{
    public partial class Second : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentID",
                table: "Matches",
                column: "TournamentID");

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
