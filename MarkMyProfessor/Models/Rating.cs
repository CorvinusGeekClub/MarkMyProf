using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarkMyProfessor.Models
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Professor Professor { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public decimal AchievableRate { get; set; }

        [Required]
        public decimal UsefulRate { get; set; }

        [Required]
        public decimal HelpfulRate { get; set; }

        [Required]
        public decimal PreparedRate { get; set; }

        [Required]
        public decimal StyleRate { get; set; }

        [Required]
        public bool IsSexy { get; set; }

    }
}
