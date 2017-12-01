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

        // GET: Tournaments
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.Tournaments.Include(t => t.Game);
            return View(await siteContext.ToListAsync());
        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "ID");
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,StartDate,EndDate,GamesID")] Tournaments tournaments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournaments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "ID", tournaments.GamesID);
            return View(tournaments);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "ID", tournaments.GamesID);
            return View(tournaments);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,StartDate,EndDate,GamesID")] Tournaments tournaments)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "ID", tournaments.GamesID);
            return View(tournaments);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournaments = await _context.Tournaments.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tournaments.Remove(tournaments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentsExists(int id)
        {
            return _context.Tournaments.Any(e => e.ID == id);
        }
    }
}
