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
        public IActionResult Index(bool? success = null)
        {
            return View(success);
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

        [HttpPost]
        public async Task<IActionResult> SeedUrl(string url)
        {
            SeedData seeder = new SeedData(_context);
            bool succ = await seeder.SeedDataFromUrlAsync(url);
            return RedirectToAction(nameof(Index), new {success = succ});
        }
    }
}