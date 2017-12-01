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
    public class TeamsPlayedController : Controller
    {
        private readonly SiteContext _context;

        public TeamsPlayedController(SiteContext context)
        {
            _context = context;
        }

        // GET: TeamsPlayeds
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.TeamsPlayed.Include(t => t.Players).Include(t => t.Teams);
            return View(await siteContext.ToListAsync());
        }

        // GET: TeamsPlayeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsPlayed = await _context.TeamsPlayed
                .Include(t => t.Players)
                .Include(t => t.Teams)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsPlayed == null)
            {
                return NotFound();
            }

            return View(teamsPlayed);
        }

        // GET: TeamsPlayeds/Create
        public IActionResult Create()
        {
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "Discriminator");
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division");
            return View();
        }

        // POST: TeamsPlayeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerID,TeamID,StartDate,EndDate,ID,Email,Username,FirstName,LastName,Joined,DateOfBirth,Password")] TeamsPlayed teamsPlayed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamsPlayed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "Discriminator", teamsPlayed.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsPlayed.TeamID);
            return View(teamsPlayed);
        }

        // GET: TeamsPlayeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsPlayed = await _context.TeamsPlayed.SingleOrDefaultAsync(m => m.ID == id);
            if (teamsPlayed == null)
            {
                return NotFound();
            }
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "Discriminator", teamsPlayed.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsPlayed.TeamID);
            return View(teamsPlayed);
        }

        // POST: TeamsPlayeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerID,TeamID,StartDate,EndDate,ID,Email,Username,FirstName,LastName,Joined,DateOfBirth,Password")] TeamsPlayed teamsPlayed)
        {
            if (id != teamsPlayed.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamsPlayed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsPlayedExists(teamsPlayed.ID))
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
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "Discriminator", teamsPlayed.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsPlayed.TeamID);
            return View(teamsPlayed);
        }

        // GET: TeamsPlayeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsPlayed = await _context.TeamsPlayed
                .Include(t => t.Players)
                .Include(t => t.Teams)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsPlayed == null)
            {
                return NotFound();
            }

            return View(teamsPlayed);
        }

        // POST: TeamsPlayeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamsPlayed = await _context.TeamsPlayed.SingleOrDefaultAsync(m => m.ID == id);
            _context.TeamsPlayed.Remove(teamsPlayed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsPlayedExists(int id)
        {
            return _context.TeamsPlayed.Any(e => e.ID == id);
        }
    }
}
