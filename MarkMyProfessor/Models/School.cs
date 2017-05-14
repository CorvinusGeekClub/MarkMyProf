using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarkMyProfessor.Models
{
    public class School
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SchoolId { get; set; }

        public string ShortName { get; set; }

        public string LongName { get; set; }
    }
}
