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
    public class AliasesController : Controller
    {
        private readonly SiteContext _context;

        public AliasesController(SiteContext context)
        {
            _context = context;
        }

        // GET: Aliases
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.Aliases.Include(a => a.Member);
            return View(await siteContext.ToListAsync());
        }

        // GET: Aliases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliases = await _context.Aliases
                .Include(a => a.Member)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (aliases == null)
            {
                return NotFound();
            }

            return View(aliases);
        }

        // GET: Aliases/Create
        public IActionResult Create()
        {
            ViewData["MembersID"] = new SelectList(_context.Members, "ID", "Discriminator");
            return View();
        }

        // POST: Aliases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MembersID,IGN")] Aliases aliases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aliases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembersID"] = new SelectList(_context.Members, "ID", "Discriminator", aliases.MembersID);
            return View(aliases);
        }

        // GET: Aliases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliases = await _context.Aliases.SingleOrDefaultAsync(m => m.ID == id);
            if (aliases == null)
            {
                return NotFound();
            }
            ViewData["MembersID"] = new SelectList(_context.Members, "ID", "Discriminator", aliases.MembersID);
            return View(aliases);
        }

        // POST: Aliases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MembersID,IGN")] Aliases aliases)
        {
            if (id != aliases.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aliases);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AliasesExists(aliases.ID))
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
            ViewData["MembersID"] = new SelectList(_context.Members, "ID", "Discriminator", aliases.MembersID);
            return View(aliases);
        }

        // GET: Aliases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliases = await _context.Aliases
                .Include(a => a.Member)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (aliases == null)
            {
                return NotFound();
            }

            return View(aliases);
        }

        // POST: Aliases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aliases = await _context.Aliases.SingleOrDefaultAsync(m => m.ID == id);
            _context.Aliases.Remove(aliases);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AliasesExists(int id)
        {
            return _context.Aliases.Any(e => e.ID == id);
        }
    }
}
