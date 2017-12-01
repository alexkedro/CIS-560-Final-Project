/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CIS_560_Final_Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;


namespace CIS_560_Final_Project.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(
            UserManager<Members> userManager,
            SignInManager<Members> signInManager,
            ILogger<AccountController> logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _logger = logger;
        }

        public UserManager<Members> UserManager { get; }
        public SignInManager<Members> 


        [Route("Account/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Account/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Members model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new Members { Username = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName};
            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Members model, string username, string password)
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
*/