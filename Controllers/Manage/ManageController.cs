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
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var roles = await _userManager.GetRolesAsync(user);
            foreach (string s in roles)
            {
                if (s.Equals("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

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

        #region Player
        [HttpGet]
        public async Task<IActionResult> Player()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var coach = await _context.Coaches.SingleOrDefaultAsync(i => i.User == user);
            var player = await _context.Players.SingleOrDefaultAsync(i => i.User == user);

            if (coach == null && player == null)
            {
                var model = new PlayerViewModel
                {
                    ID = default(int),
                    FirstName = "",
                    LastName = "",
                    DateOfBirth = DateTime.UtcNow,
                    Joined = DateTime.UtcNow,
                    cop = CoachOrPlayer.Member
                };
                return View(model);
            }

            ViewBag.pYears = new SelectList(Enum.GetValues(typeof(CIS_560_Final_Project.Entities.YearClassification)));

            if (coach != null)
            {
                var model = new PlayerViewModel
                {
                    ID = coach.ID,
                    FirstName = coach.FirstName,
                    LastName = coach.LastName,
                    DateOfBirth = coach.DateOfBirth,
                    Joined = coach.Joined,
                    IsManager = coach.IsManager,
                    YearsCoaching = coach.YearsCoaching,
                    cop = CoachOrPlayer.Coach
                };
                return View(model);
            }
            else
            {
                var model = new PlayerViewModel
                {
                    ID = player.ID,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    DateOfBirth = player.DateOfBirth,
                    Joined = player.Joined,
                    IGN = player.IGN,
                    Year = player.Year,
                    cop = CoachOrPlayer.Player
                };
                return View(model);
            }

        }


        [HttpPost, ActionName("SelectRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Player(PlayerViewModel model, int id)
        {
            var user = await _userManager.GetUserAsync(User);

            //Add Coach
            if (id == 1)
            {
                var coach = new Coaches
                {
                    FirstName = "",
                    LastName = "",
                    User = user,
                    Joined = DateTime.UtcNow,
                    DateOfBirth = DateTime.UtcNow,
                    YearsCoaching = 0,
                    IsManager = false
                };

                await _context.AddAsync(coach);
                await _context.SaveChangesAsync();

                StatusMessage = "You have been set as a Coach!";
                return RedirectToAction(nameof(Player));
            }
            //Add player
            else
            {
                var player = new Players
                {
                    FirstName = "",
                    LastName = "",
                    Joined = DateTime.UtcNow,
                    DateOfBirth = DateTime.UtcNow,
                    User = user,
                    IGN = "",
                    Year = model.Year
                };

                await _context.AddAsync(player);
                await _context.SaveChangesAsync();

                StatusMessage = "You have been set as a Coach!";
                return RedirectToAction(nameof(Player));
            }

        }

        //Only can happen if user is coach or player
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Player(PlayerViewModel model)
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

            //update
            var playerUpdate = await _context.Players.SingleOrDefaultAsync(i => i.User == user);
            var coachUpdate = await _context.Coaches.SingleOrDefaultAsync(i => i.User == user);

            if (playerUpdate != null)
            {
                playerUpdate.FirstName = model.FirstName;
                playerUpdate.LastName = model.LastName;
                playerUpdate.DateOfBirth = model.DateOfBirth;
                playerUpdate.IGN = model.IGN;
                playerUpdate.Year = model.Year;
                _context.SaveChanges();

                StatusMessage = "Your profile has been updated";
                return RedirectToAction(nameof(Index));
            }

            else if (coachUpdate != null)
            {
                coachUpdate.FirstName = model.FirstName;
                coachUpdate.LastName = model.LastName;
                coachUpdate.DateOfBirth = model.DateOfBirth;
                coachUpdate.IsManager = model.IsManager;
                coachUpdate.YearsCoaching = model.YearsCoaching;
                _context.SaveChanges();

                StatusMessage = "Your profile has been updated";
                return RedirectToAction(nameof(Player));
            }

            return View(model);
        }



        public async Task<IActionResult> DeleteAccount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mem = await _context.Members.SingleOrDefaultAsync(m => m.ID == id);

            if(mem == null)
            {
                return NotFound();
            }

            return View(mem);
        }

        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(int id)
        {            
            var mem = await _context.Members.SingleOrDefaultAsync(m => m.ID == id);
            var user = await _userManager.GetUserAsync(User);

            
            _context.Remove(mem);
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);

            _logger.LogInformation("User Deleted.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost, ActionName("DeleteCancel")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCancel()
        {
            return RedirectToAction("Player");
        }
        #endregion

        #region Team

        [HttpGet]
        public async Task<IActionResult> Team()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var coach = await _context.Coaches.SingleOrDefaultAsync(i => i.User == user);
            var player = await _context.Players.SingleOrDefaultAsync(i => i.User == user);

            if (coach == null && player == null)
            {
                StatusMessage = "Need to become fill out user information first";
                return RedirectToAction(nameof(Player));
            }

            //if player
            if(coach == null)
            {
                var team = await _context.TeamsMembers.SingleOrDefaultAsync(i => i.Member == player);
                if(team == null)
                {
                    StatusMessage = "Not part of a team yet. Wait until a coach adds you";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                   return RedirectToAction("Index", "TeamController", new { id = team.ID }); 
                }
            }

            //if couch
            if(player == null)
            {
                //var playersInTeam = await _context.TeamsMembers.ToListAsync(); 
                var playersNotInTeam = await _context.Players.Where(p => p.TeamsMembers.Count() == 0).ToListAsync();
                return View(playersNotInTeam);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");
                return Redirect(nameof(Index));
            }
        }

        [HttpPost, ActionName("TeamAdd")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Team(int? id)
        {
            var player = await _context.Players.SingleOrDefaultAsync(i => i.ID == id);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var coach = await _context.Coaches.SingleOrDefaultAsync(i => i.User == user);
            var coachTeam = await _context.TeamsMembers.SingleOrDefaultAsync(i => i.Member == coach);

            if(coachTeam == null)
            {
                StatusMessage = "Your profile has been updated";
                return RedirectToAction(nameof(TeamRegister));
            }

            var teamsMembers = new TeamsMembers
            {
                Member = player,
                Team = coachTeam.Team,
            };

            await _context.TeamsMembers.AddAsync(teamsMembers);
            var result = await _context.SaveChangesAsync();
            StatusMessage = "Player " + player.FullName + " Added to team";
            return RedirectToAction(nameof(Team));
        }

        [HttpGet]
        public async Task<IActionResult> TeamRegister()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var schools = await _context.Schools.ToListAsync();
            var games = await _context.Games.ToListAsync();

            var model = new TeamViewModel
            {
                Schools = schools,
                Games = games,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamRegister(TeamViewModel model, string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var coach = await _context.Coaches.SingleOrDefaultAsync(i => i.User == user);
            var newteam = new Teams
            {
                School = model.Schools.FirstOrDefault(),
                Game = model.Games.FirstOrDefault(),
                Name = model.Name
            };
            

            await _context.AddAsync(newteam);
            await _context.SaveChangesAsync();

            var team = await _context.Teams.SingleOrDefaultAsync(i => i.Name == newteam.Name);

            var teamsMemebers = new TeamsMembers
            {
                Team = team,
                Member = coach
            };

            await _context.AddAsync(teamsMemebers);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Team));
        }

        #endregion

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
