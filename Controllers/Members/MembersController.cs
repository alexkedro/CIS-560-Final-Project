using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS_560_Final_Project.Models;
using CIS_560_Final_Project.Models.ManageViewModels;
using CIS_560_Final_Project.Entities;

namespace CIS_560_Final_Project.Controllers
{
    public class MembersController : Controller
    {
        private readonly SiteContext _context;

        public MembersController(SiteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                List<PlayerViewModel> teammems = new List<PlayerViewModel>();
                var siteContext = _context.TeamsMembers.Include(tm => tm.Member).Include(tm => tm.Team).Where(tm => tm.Team.ID == id);
                IEnumerable<TeamsMembers> members = await siteContext.ToListAsync();
                foreach (TeamsMembers tm in members)
                {
                    var coach = await _context.Coaches.SingleOrDefaultAsync(i => i.ID == tm.Member.ID);
                    var player = await _context.Players.SingleOrDefaultAsync(i => i.ID == tm.Member.ID);

                    if (coach != null)
                    {
                        teammems.Add(new PlayerViewModel
                        {
                            ID = coach.ID,
                            FirstName = coach.FirstName,
                            LastName = coach.LastName,
                            cop = CoachOrPlayer.Coach
                        });
                    }
                    else if (player != null)
                    {
                        teammems.Add(new PlayerViewModel
                        {
                            ID = player.ID,
                            FirstName = player.FirstName,
                            LastName = player.LastName,
                            cop = CoachOrPlayer.Player
                        });
                    }
                }
                ViewBag.tname = _context.Teams.Single(t => t.ID == id).Name;
                ViewBag.tid = id;
                return View(teammems);
            }
        }

        public IActionResult ReturnToTeams(int id)
        {
            return RedirectToAction("Index", "Teams", new { id = id });
        }
    }
}