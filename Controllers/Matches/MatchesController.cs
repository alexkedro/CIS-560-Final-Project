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
    public class MatchesController : Controller
    {
        private readonly SiteContext _context;

        public MatchesController(SiteContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.Matches.Include(m => m.Team1).Include(m => m.Team2).Include(m => m.Tournament);
            return View(await siteContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Tournament)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division");
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division");
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MatchNumber,Team1ID,Team2ID,Winner,Datetime,TournamentID")] Matches matches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division", matches.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division", matches.Team2ID);
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name", matches.TournamentID);
            return View(matches);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Matches.SingleOrDefaultAsync(m => m.ID == id);
            if (matches == null)
            {
                return NotFound();
            }
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division", matches.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division", matches.Team2ID);
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name", matches.TournamentID);
            return View(matches);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MatchNumber,Team1ID,Team2ID,Winner,Datetime,TournamentID")] Matches matches)
        {
            if (id != matches.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchesExists(matches.ID))
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
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division", matches.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division", matches.Team2ID);
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name", matches.TournamentID);
            return View(matches);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Tournament)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matches = await _context.Matches.SingleOrDefaultAsync(m => m.ID == id);
            _context.Matches.Remove(matches);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchesExists(int id)
        {
            return _context.Matches.Any(e => e.ID == id);
        }
    }
}
