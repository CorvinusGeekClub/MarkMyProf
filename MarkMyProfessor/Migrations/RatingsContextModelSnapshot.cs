using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MarkMyProfessor.Models;

namespace MarkMyProfessor.Migrations
{
    [DbContext(typeof(RatingsContext))]
    partial class RatingsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarkMyProfessor.Models.Professor", b =>
                {
                    b.Property<int>("ProfessorId");

                    b.Property<string>("MigratedCourses");

                    b.Property<bool>("MigratedIsSexy");

                    b.Property<decimal>("MigratedRateAchievable");

                    b.Property<decimal>("MigratedRateHelpful");

                    b.Property<decimal>("MigratedRatePrepared");

                    b.Property<decimal>("MigratedRateStyle");

                    b.Property<decimal>("MigratedRateUseful");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SchoolId");

                    b.HasKey("ProfessorId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("MarkMyProfessor.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AchievableRate");

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<string>("Course")
                        .IsRequired();

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("HelpfulRate");

                    b.Property<bool>("IsSexy");

                    b.Property<decimal>("PreparedRate");

                    b.Property<int>("ProfessorId");

                    b.Property<decimal>("StyleRate");

                    b.Property<decimal>("UsefulRate");

                    b.HasKey("RatingId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("MarkMyProfessor.Models.School", b =>
                {
                    b.Property<int>("SchoolId");

                    b.Property<string>("LongName");

                    b.Property<string>("ShortName");

                    b.HasKey("SchoolId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("MarkMyProfessor.Models.Professor", b =>
                {
                    b.HasOne("MarkMyProfessor.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MarkMyProfessor.Models.Rating", b =>
                {
                    b.HasOne("MarkMyProfessor.Models.Professor", "Professor")
                        .WithMany("Ratings")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
