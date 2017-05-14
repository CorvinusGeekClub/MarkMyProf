using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarkMyProfessor.Migrations
{
    public partial class RatingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Ratings",
                newName: "Course");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Ratings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSexy",
                table: "Ratings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsSexy",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Ratings",
                newName: "Subject");
        }
    }
}
