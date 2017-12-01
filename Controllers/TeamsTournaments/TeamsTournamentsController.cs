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
    public class TeamsTournamentsController : Controller
    {
        private readonly SiteContext _context;

        public TeamsTournamentsController(SiteContext context)
        {
            _context = context;
        }

        // GET: TeamsTournaments
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.TeamsTournaments.Include(t => t.Team).Include(t => t.Tournament);
            return View(await siteContext.ToListAsync());
        }

        // GET: TeamsTournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsTournaments = await _context.TeamsTournaments
                .Include(t => t.Team)
                .Include(t => t.Tournament)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsTournaments == null)
            {
                return NotFound();
            }

            return View(teamsTournaments);
        }

        // GET: TeamsTournaments/Create
        public IActionResult Create()
        {
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division");
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name");
            return View();
        }

        // POST: TeamsTournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TeamID,TournamentID")] TeamsTournaments teamsTournaments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamsTournaments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsTournaments.TeamID);
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name", teamsTournaments.TournamentID);
            return View(teamsTournaments);
        }

        // GET: TeamsTournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsTournaments = await _context.TeamsTournaments.SingleOrDefaultAsync(m => m.ID == id);
            if (teamsTournaments == null)
            {
                return NotFound();
            }
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsTournaments.TeamID);
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name", teamsTournaments.TournamentID);
            return View(teamsTournaments);
        }

        // POST: TeamsTournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TeamID,TournamentID")] TeamsTournaments teamsTournaments)
        {
            if (id != teamsTournaments.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamsTournaments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsTournamentsExists(teamsTournaments.ID))
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
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsTournaments.TeamID);
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name", teamsTournaments.TournamentID);
            return View(teamsTournaments);
        }

        // GET: TeamsTournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsTournaments = await _context.TeamsTournaments
                .Include(t => t.Team)
                .Include(t => t.Tournament)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsTournaments == null)
            {
                return NotFound();
            }

            return View(teamsTournaments);
        }

        // POST: TeamsTournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamsTournaments = await _context.TeamsTournaments.SingleOrDefaultAsync(m => m.ID == id);
            _context.TeamsTournaments.Remove(teamsTournaments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsTournamentsExists(int id)
        {
            return _context.TeamsTournaments.Any(e => e.ID == id);
        }
    }
}
