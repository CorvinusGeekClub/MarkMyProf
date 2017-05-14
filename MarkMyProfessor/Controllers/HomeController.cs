using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkMyProfessor.Models;
using MarkMyProfessor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkMyProfessor.Controllers
{
    public class HomeController : Controller
    {
        private readonly RatingsContext _context;

        public HomeController(RatingsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel vm = new IndexViewModel();
            vm.Professors = await _context.Professors.Include(p => p.School)
                .Select(p => new ProfessorViewModel()
                {
                    AchievableRate = p.MigratedRateAchievable,
                    Name = p.Name,
                    School =  p.School.ShortName,
                    StyleRate = p.MigratedRateStyle,
                    UsefulRate = p.MigratedRateUseful,
                    HelpfulRate = p.MigratedRateHelpful,
                    PreparedRate = p.MigratedRatePrepared
                }).ToListAsync();
            return View(vm);
        }
    }
}
