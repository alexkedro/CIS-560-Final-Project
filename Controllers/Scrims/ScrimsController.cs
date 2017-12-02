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
    public class ScrimsController : Controller
    {
        private readonly SiteContext _context;

        public ScrimsController(SiteContext context)
        {
            _context = context;
        }

        // GET: Scrims
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.Scrims.Include(s => s.Team1).Include(s => s.Team2);
            return View(await siteContext.ToListAsync());
        }

        // GET: Scrims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrims = await _context.Scrims
                .Include(s => s.Team1)
                .Include(s => s.Team2)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (scrims == null)
            {
                return NotFound();
            }

            return View(scrims);
        }

        // GET: Scrims/Create
        public IActionResult Create()
        {
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division");
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division");
            return View();
        }

        // POST: Scrims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Team1ID,Team2ID,Score1,Score2,Winner,Datetime")] Scrims scrims)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scrims);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division", scrims.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division", scrims.Team2ID);
            return View(scrims);
        }

        // GET: Scrims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrims = await _context.Scrims.SingleOrDefaultAsync(m => m.ID == id);
            if (scrims == null)
            {
                return NotFound();
            }
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division", scrims.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division", scrims.Team2ID);
            return View(scrims);
        }

        // POST: Scrims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Team1ID,Team2ID,Score1,Score2,Winner,Datetime")] Scrims scrims)
        {
            if (id != scrims.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scrims);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScrimsExists(scrims.ID))
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
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Division", scrims.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Division", scrims.Team2ID);
            return View(scrims);
        }

        // GET: Scrims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrims = await _context.Scrims
                .Include(s => s.Team1)
                .Include(s => s.Team2)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (scrims == null)
            {
                return NotFound();
            }

            return View(scrims);
        }

        // POST: Scrims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scrims = await _context.Scrims.SingleOrDefaultAsync(m => m.ID == id);
            _context.Scrims.Remove(scrims);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScrimsExists(int id)
        {
            return _context.Scrims.Any(e => e.ID == id);
        }
    }
}
