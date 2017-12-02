﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS_560_Final_Project.Models;

namespace CIS_560_Final_Project.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly SiteContext _context;

        public SchoolsController(SiteContext context)
        {
            _context = context;
        }

        // GET: Schools
        public async Task<IActionResult> Index()
        {
            return View(await _context.Schools.ToListAsync());
        }

        // GET: Schools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schools = await _context.Schools
                .SingleOrDefaultAsync(m => m.ID == id);
            if (schools == null)
            {
                return NotFound();
            }

            return View(schools);
        }

        // GET: Schools/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Address1,Address2,City,State,Name,Population")] Schools schools)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schools);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schools);
        }

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schools = await _context.Schools.SingleOrDefaultAsync(m => m.ID == id);
            if (schools == null)
            {
                return NotFound();
            }
            return View(schools);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Address1,Address2,City,State,Name,Population")] Schools schools)
        {
            if (id != schools.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schools);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolsExists(schools.ID))
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
            return View(schools);
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schools = await _context.Schools
                .SingleOrDefaultAsync(m => m.ID == id);
            if (schools == null)
            {
                return NotFound();
            }

            return View(schools);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schools = await _context.Schools.SingleOrDefaultAsync(m => m.ID == id);
            _context.Schools.Remove(schools);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolsExists(int id)
        {
            return _context.Schools.Any(e => e.ID == id);
        }
    }
}
