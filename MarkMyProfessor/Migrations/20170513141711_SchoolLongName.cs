using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarkMyProfessor.Migrations
{
    public partial class SchoolLongName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolShortName",
                table: "Schools",
                newName: "ShortName");

            migrationBuilder.AddColumn<string>(
                name: "LongName",
                table: "Schools",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongName",
                table: "Schools");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Schools",
                newName: "SchoolShortName");
        }
    }
}
