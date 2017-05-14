using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkMyProfessor.ViewModels
{
    public class ProfessorViewModel
    {
        public string Name { get; set; }

        public string School { get; set; }

        public decimal AchievableRate { get; set; }

        public decimal UsefulRate { get; set; }

        public decimal HelpfulRate { get; set; }

        public decimal StyleRate { get; set; }

        public decimal PreparedRate { get; set; }
    }
}
