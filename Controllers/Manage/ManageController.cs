using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CIS_560_Final_Project.Models;
using CIS_560_Final_Project.Models.ManageViewModels;
using CIS_560_Final_Project.Entities;

namespace CIS_560_Final_Project.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly SiteContext _context;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
          UserManager<Users> userManager,
          SignInManager<Users> signInManager,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder,
          SiteContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Player(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var coach = await _context.Coaches.SingleOrDefaultAsync(i => i.User == user);
            var player = await _context.Players.SingleOrDefaultAsync(i => i.User == user);

            if(coach == null && player == null)
            {
                return View();
            }

            if(coach != null)
            {
                var model = new PlayerViewModel
                {
                    FirstName = coach.FirstName,
                    LastName = coach.LastName,
                    DateOfBirth = coach.DateOfBirth,
                    IsManager = coach.IsManager,
                    YearsCoaching = coach.YearsCoaching
                };
                return View(model);
            }
            else
            {
                var alias = await _context.Aliases.SingleOrDefaultAsync(i => i.ID == player.Alias.ID);
                var model = new PlayerViewModel
                {
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    DateOfBirth = player.DateOfBirth,
                    IGN = alias.IGN,
                    Year = player.Year,
                };
                return View(model);
            }
            
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Player(PlayerViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(i => i.User == user);

            //Create
            if(member == null)
            {
                //Add Coach
                if(model.cop == CoachOrPlayer.Coach){
                    var coach = new Coaches{
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Joined = DateTime.UtcNow,
                        DateOfBirth = model.DateOfBirth,
                        YearsCoaching = model.YearsCoaching,
                        IsManager = model.IsManager
                    };

                    await _context.AddAsync(coach);
                    await _context.SaveChangesAsync();

                    StatusMessage = "Your profile has been updated";
                    return RedirectToAction(nameof(Player));
                }
                //Add player
                else if (model.cop == CoachOrPlayer.Player) {
                    var alias = new Aliases{
                        IGN = model.IGN
                    };

                    var player = new Players{
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Joined = DateTime.UtcNow,
                        DateOfBirth = model.DateOfBirth,
                        Alias = alias,
                        Year = model.Year
                    };

                    await _context.AddAsync(alias);
                    await _context.AddAsync(player);
                    await _context.SaveChangesAsync();

                    StatusMessage = "Your profile has been updated";
                    return RedirectToAction(nameof(Player));
                }
            }
            //update
            else {
                var playerUpdate = await _context.Players.SingleOrDefaultAsync(i => i.User == user);
                var coachUpdate = await _context.Coaches.SingleOrDefaultAsync(i => i.User == user);

                if(playerUpdate != null)
                {
                    var alias = new Aliases{
                        IGN = model.IGN
                    };

                    var player = new Players{
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Alias = alias,
                        Year = model.Year
                    };

                    _context.Update(alias);
                    _context.Update(player);
                    _context.SaveChanges();


                    StatusMessage = "Your profile has been updated";
                    return RedirectToAction(nameof(Index));
                }
                else if (coachUpdate != null)
                {
                    var coach = new Coaches{
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        YearsCoaching = model.YearsCoaching,
                        IsManager = model.IsManager
                    };

                    _context.Update(coach);
                    _context.SaveChanges();

                    StatusMessage = "Your profile has been updated";
                    return RedirectToAction(nameof(Player));
                }
            }
            return View(model);
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
