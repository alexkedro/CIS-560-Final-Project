/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS_560_Final_Project.Models;

namespace CIS_560_Final_Project.Controllers
{
    public class TeamsCoachedController : Controller
    {
        private readonly SiteContext _context;

        public TeamsCoachedController(SiteContext context)
        {
            _context = context;
        }

        // GET: TeamsCoacheds
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.TeamsCoached.Include(t => t.Coach).Include(t => t.Team);
            return View(await siteContext.ToListAsync());
        }

        // GET: TeamsCoacheds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsCoached = await _context.TeamsCoached
                .Include(t => t.Coach)
                .Include(t => t.Team)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsCoached == null)
            {
                return NotFound();
            }

            return View(teamsCoached);
        }

        // GET: TeamsCoacheds/Create
        public IActionResult Create()
        {
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "Discriminator");
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division");
            return View();
        }

        // POST: TeamsCoacheds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoachID,TeamID,WasManager,StartDate,EndDate,ID,Email,Username,FirstName,LastName,Joined,DateOfBirth,Password")] TeamsCoached teamsCoached)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamsCoached);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "Discriminator", teamsCoached.CoachID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsCoached.TeamID);
            return View(teamsCoached);
        }

        // GET: TeamsCoacheds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsCoached = await _context.TeamsCoached.SingleOrDefaultAsync(m => m.ID == id);
            if (teamsCoached == null)
            {
                return NotFound();
            }
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "Discriminator", teamsCoached.CoachID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsCoached.TeamID);
            return View(teamsCoached);
        }

        // POST: TeamsCoacheds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoachID,TeamID,WasManager,StartDate,EndDate,ID,Email,Username,FirstName,LastName,Joined,DateOfBirth,Password")] TeamsCoached teamsCoached)
        {
            if (id != teamsCoached.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamsCoached);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsCoachedExists(teamsCoached.ID))
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
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "Discriminator", teamsCoached.CoachID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Division", teamsCoached.TeamID);
            return View(teamsCoached);
        }

        // GET: TeamsCoacheds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsCoached = await _context.TeamsCoached
                .Include(t => t.Coach)
                .Include(t => t.Team)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsCoached == null)
            {
                return NotFound();
            }

            return View(teamsCoached);
        }

        // POST: TeamsCoacheds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamsCoached = await _context.TeamsCoached.SingleOrDefaultAsync(m => m.ID == id);
            _context.TeamsCoached.Remove(teamsCoached);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsCoachedExists(int id)
        {
            return _context.TeamsCoached.Any(e => e.ID == id);
        }
    }
}
*/