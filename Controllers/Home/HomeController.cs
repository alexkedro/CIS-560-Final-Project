﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CIS_560_Final_Project.Models;

namespace CIS_560_Final_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Tournaments()
        {
            return RedirectToAction("Index", "Tournaments");
        }
        public IActionResult Schools()
        {
            return RedirectToAction("Index", "Schools");
        }
        public IActionResult Teams()
        {
            return RedirectToAction("Index", "Teams");
        }
        public IActionResult Scrims()
        {
            return RedirectToAction("Index", "Scrims");
        }

    }
}
