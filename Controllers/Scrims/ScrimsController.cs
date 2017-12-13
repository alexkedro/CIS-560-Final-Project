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
namespace CIS_560_Final_Project.Controllers.Scrims
{
    public class ScrimsController : Controller
    {
        private readonly SiteContext _context;

        public ScrimsController(SiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var siteContext = _context.Scrims.Include(s => s.Team1).Include(s => s.Team2);
            return View(await siteContext.ToListAsync());
        }

        public IActionResult ReturnToHome()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}