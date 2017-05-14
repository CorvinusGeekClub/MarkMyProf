using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkMyProfessor.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarkMyProfessor.Controllers
{
    public class ArchiveController : Controller
    {
        private RatingsContext _context;

        public ArchiveController(RatingsContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> FetchData()
        {
            SeedData seeder = new SeedData(_context);
            await seeder.SeedDataAsync();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }
    }
}