using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS_560_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using CIS_560_Final_Project.Entities;
using CIS_560_Final_Project.Models.ManageViewModels;

namespace CIS_560_Final_Project.Controllers
{
    public class TeamsController : Controller
    {
        private readonly SiteContext _context;

        public TeamsController(SiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var siteContext = _context.Teams.Include(t => t.School).Include(t => t.Game);
                ViewBag.search = false;
                return View(await siteContext.ToListAsync());
            }
            else
            {
                var siteContext = _context.Teams.Include(t => t.School).Include(t => t.Game).Where(m => m.School.ID == id);
                ViewBag.search = true;
                ViewBag.tname = _context.Schools.Single(s => s.ID == id).Name;
                ViewBag.tid = id;
                return View(await siteContext.ToListAsync());
            }
        }

        public IActionResult ReturnToSchools()
        {
            return RedirectToAction("Index", "Schools");
        }

        public IActionResult TeamDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Members", new { id = id });
        }
    }
}