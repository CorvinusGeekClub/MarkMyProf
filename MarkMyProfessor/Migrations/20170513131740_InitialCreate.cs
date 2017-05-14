using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MarkMyProfessor.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(nullable: false),
                    SchoolShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    ProfessorId = table.Column<int>(nullable: false),
                    MigratedCourses = table.Column<string>(nullable: true),
                    MigratedIsSexy = table.Column<bool>(nullable: false),
                    MigratedRateAchievable = table.Column<decimal>(nullable: false),
                    MigratedRateHelpful = table.Column<decimal>(nullable: false),
                    MigratedRatePrepared = table.Column<decimal>(nullable: false),
                    MigratedRateStyle = table.Column<decimal>(nullable: false),
                    MigratedRateUseful = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.ProfessorId);
                    table.ForeignKey(
                        name: "FK_Professors_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(nullable: false),
                    AchievableRate = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HelpfulRate = table.Column<decimal>(nullable: false),
                    PreparedRate = table.Column<decimal>(nullable: false),
                    ProfessorId = table.Column<int>(nullable: false),
                    StyleRate = table.Column<decimal>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    UsefulRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "ProfessorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professors_SchoolId",
                table: "Professors",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProfessorId",
                table: "Ratings",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
