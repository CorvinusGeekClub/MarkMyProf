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
            await seeder.SeedDataWaybackMachineAsync();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }

        public async Task<IActionResult> FetchGoogle()
        {
            SeedData seeder = new SeedData(_context);
            await seeder.SeedDataGoogleCacheAsync();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }
    }
}