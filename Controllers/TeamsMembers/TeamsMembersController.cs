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
    public class TeamsMembersController : Controller
    {
        private readonly SiteContext _context;

        public TeamsMembersController(SiteContext context)
        {
            _context = context;
        }

        // GET: TeamsMembers
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.TeamsMembers.Include(t => t.Player).Include(t => t.Team);
            return View(await siteContext.ToListAsync());
        }

        // GET: TeamsMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsMembers = await _context.TeamsMembers
                .Include(t => t.Player)
                .Include(t => t.Team)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsMembers == null)
            {
                return NotFound();
            }

            return View(teamsMembers);
        }

        // GET: TeamsMembers/Create
        public IActionResult Create()
        {
            ViewData["PlayersID"] = new SelectList(_context.Players, "ID", "Discriminator");
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Division");
            return View();
        }

        // POST: TeamsMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TeamsID,PlayersID")] TeamsMembers teamsMembers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamsMembers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayersID"] = new SelectList(_context.Players, "ID", "Discriminator", teamsMembers.PlayersID);
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Division", teamsMembers.TeamsID);
            return View(teamsMembers);
        }

        // GET: TeamsMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsMembers = await _context.TeamsMembers.SingleOrDefaultAsync(m => m.ID == id);
            if (teamsMembers == null)
            {
                return NotFound();
            }
            ViewData["PlayersID"] = new SelectList(_context.Players, "ID", "Discriminator", teamsMembers.PlayersID);
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Division", teamsMembers.TeamsID);
            return View(teamsMembers);
        }

        // POST: TeamsMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TeamsID,PlayersID")] TeamsMembers teamsMembers)
        {
            if (id != teamsMembers.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamsMembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsMembersExists(teamsMembers.ID))
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
            ViewData["PlayersID"] = new SelectList(_context.Players, "ID", "Discriminator", teamsMembers.PlayersID);
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Division", teamsMembers.TeamsID);
            return View(teamsMembers);
        }

        // GET: TeamsMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamsMembers = await _context.TeamsMembers
                .Include(t => t.Player)
                .Include(t => t.Team)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teamsMembers == null)
            {
                return NotFound();
            }

            return View(teamsMembers);
        }

        // POST: TeamsMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamsMembers = await _context.TeamsMembers.SingleOrDefaultAsync(m => m.ID == id);
            _context.TeamsMembers.Remove(teamsMembers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsMembersExists(int id)
        {
            return _context.TeamsMembers.Any(e => e.ID == id);
        }
    }
}
