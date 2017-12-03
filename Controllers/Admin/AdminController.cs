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
    public class AdminController : Controller
    {
        private readonly SiteContext _context;

        public AdminController(SiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Tournament Stuff

        public async Task<IActionResult> Tournaments()
        {
            var siteContext = _context.Tournaments.Include(t => t.Game);
            return View(await siteContext.ToListAsync());
        }

        public IActionResult CreateTournament()
        {
           
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTournament([Bind("ID,Name,StartDate,EndDate,GamesID")] Tournaments tournaments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournaments);
                await _context.SaveChangesAsync();
                ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
                return RedirectToAction("Tournaments");
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
            return View(tournaments);
        }

        public async Task<IActionResult> EditTournament(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournaments = await _context.Tournaments.SingleOrDefaultAsync(m => m.ID == id);
            if (tournaments == null)
            {
                return NotFound();
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
            return View(tournaments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTournament(int id, [Bind("ID,Name,StartDate,EndDate,GamesID")] Tournaments tournaments)
        {
            if (id != tournaments.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournaments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentsExists(tournaments.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Tournaments");
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
            return View(tournaments);
        }

        private bool TournamentsExists(int id)
        {
            return _context.Tournaments.Any(e => e.ID == id);
        }

        public async Task<IActionResult> DeleteTournament(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournaments = await _context.Tournaments
                .Include(t => t.Game)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tournaments == null)
            {
                return NotFound();
            }

            return View(tournaments);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTournamentConfirmed(int id)
        {
            var tournaments = await _context.Tournaments.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tournaments.Remove(tournaments);
            await _context.SaveChangesAsync();
            return RedirectToAction("Tournaments");
        }

        #endregion
    }
}