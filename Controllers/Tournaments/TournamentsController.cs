using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS_560_Final_Project.Models;

namespace CIS_560_Final_Project.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly SiteContext _context;

        public TournamentsController(SiteContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.Tournaments.Include(t => t.Game);
            return View(await siteContext.ToListAsync());
        }

        public IActionResult TournamentDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Matches", new { id = id });
        }

        public IActionResult ReturnToHome()
        { 
            return RedirectToAction("Index", "Home");
        }
    }
}
