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
    public class SchoolsController : Controller
    {
        private readonly SiteContext _context;

        public SchoolsController(SiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Schools.ToListAsync());
        }

        public IActionResult SchoolDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Teams", new { id = id });
        }

        public IActionResult ReturnToHome()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}