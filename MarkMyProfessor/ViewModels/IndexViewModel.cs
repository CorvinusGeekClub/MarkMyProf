using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkMyProfessor.Models;
using Newtonsoft.Json;

namespace MarkMyProfessor.ViewModels
{
    public class IndexViewModel
    {
        public IReadOnlyList<ProfessorViewModel> Professors { get; set; }

        public string DataJson => JsonConvert.SerializeObject(Professors);
    }
}
