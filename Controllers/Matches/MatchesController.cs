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

namespace CIS_560_Final_Project.Controllers.Matches
{
    public class MatchesController : Controller
    {
        private readonly SiteContext _context;
        public MatchesController(SiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {

            if (id == null)
            {
                var siteContext = _context.Matches.Include(m => m.Team1).Include(m => m.Team2).Include(m => m.Tournament);
                ViewBag.search = false;
                return View(await siteContext.ToListAsync());
            }
            else
            {
                var siteContext = _context.Matches.Include(m => m.Team1).Include(m => m.Team2).Include(m => m.Tournament).Where(m => m.Tournament.ID == id);
                ViewBag.search = true;
                ViewBag.tname = _context.Tournaments.Single(t => t.ID == id).Name;
                ViewBag.tid = id;
                return View(await siteContext.ToListAsync());
            }
        }

        public IActionResult BackToTournaments()
        {
            return RedirectToAction("Index", "Tournaments");
        }
    }
}