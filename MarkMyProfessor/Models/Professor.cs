using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarkMyProfessor.Models
{
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfessorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public School School { get; set; }

        public int SchoolId { get; set; }

        public decimal MigratedRateAchievable { get; set; }
        public decimal MigratedRateUseful { get; set; }
        public decimal MigratedRateHelpful { get; set; }
        public decimal MigratedRatePrepared { get; set; }
        public decimal MigratedRateStyle { get; set; }
        public bool MigratedIsSexy { get; set; }
        public string MigratedCourses { get; set; }

        public List<Rating> Ratings { get; set; }

        public IEnumerable<string> Courses
        {
            get
            {
                return string.IsNullOrEmpty(MigratedCourses) ? Enumerable.Empty<string>() : MigratedCourses.Split(',');
            }
        }


    }
}
