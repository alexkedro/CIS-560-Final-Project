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

namespace CIS_560_Final_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly SiteContext _context;

        public AdminController(SiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Tournament Stuff

        public async Task<IActionResult> Tournaments()
        {
            var siteContext = _context.Tournaments.Include(t => t.Game);
            return View(await siteContext.ToListAsync());
        }

        public IActionResult CreateTournament()
        {

            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTournament([Bind("ID,Name,StartDate,EndDate,GamesID")] Tournaments tournaments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournaments);
                await _context.SaveChangesAsync();
                ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
                return RedirectToAction("Tournaments");
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
            return View(tournaments);
        }

        public async Task<IActionResult> EditTournament(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournaments = await _context.Tournaments.SingleOrDefaultAsync(m => m.ID == id);
            if (tournaments == null)
            {
                return NotFound();
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
            return View(tournaments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTournament(int id, [Bind("ID,Name,StartDate,EndDate,GamesID")] Tournaments tournaments)
        {
            if (id != tournaments.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournaments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentsExists(tournaments.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Tournaments");
            }
            ViewData["GamesID"] = new SelectList(_context.Games, "ID", "name", tournaments.GamesID);
            return View(tournaments);
        }

        private bool TournamentsExists(int id)
        {
            return _context.Tournaments.Any(e => e.ID == id);
        }

        public async Task<IActionResult> DeleteTournament(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournaments = await _context.Tournaments
                .Include(t => t.Game)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tournaments == null)
            {
                return NotFound();
            }

            return View(tournaments);
        }

        [HttpPost, ActionName("DeleteTournament")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTournamentConfirmed(int id)
        {
            var tournaments = await _context.Tournaments.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tournaments.Remove(tournaments);
            await _context.SaveChangesAsync();
            return RedirectToAction("Tournaments");
        }

        public async Task<IActionResult> TournamentDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Matches", new { id = id });

            var matches = _context.Matches.Include(m => m.Team1).Include(m => m.Team2).Include(m => m.Tournament)
                .Where(m => m.TournamentID == id);
            if (matches == null)
            {
                return NotFound();
            }
            ViewBag.data1 = _context.Tournaments.Include(t => t.Game).Single(t => t.ID == id);
            return View(await matches.ToListAsync());
        }

        #endregion

        #region Game Stuff

        public async Task<IActionResult> Games()
        {
            return View(await _context.Games.ToListAsync());
        }

        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGame([Bind("ID,name,Abv,Company")] Games games)
        {
            if (ModelState.IsValid)
            {
                _context.Add(games);
                await _context.SaveChangesAsync();
                return RedirectToAction("Games");
            }
            return View(games);
        }

        public async Task<IActionResult> EditGame(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games.SingleOrDefaultAsync(m => m.ID == id);
            if (games == null)
            {
                return NotFound();
            }
            return View(games);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGame(int id, [Bind("ID,name,Abv,Company")] Games games)
        {
            if (id != games.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(games);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesExists(games.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Games");
            }
            return View(games);
        }

        public async Task<IActionResult> DeleteGame(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .SingleOrDefaultAsync(m => m.ID == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        [HttpPost, ActionName("DeleteGame")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGameConfirmed(int id)
        {
            var games = await _context.Games.SingleOrDefaultAsync(m => m.ID == id);
            _context.Games.Remove(games);
            await _context.SaveChangesAsync();
            return RedirectToAction("Games");
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }

        #endregion

        #region School Stuff

        public async Task<IActionResult> Schools()
        {
            return View(await _context.Schools.ToListAsync());
        }

        public IActionResult CreateSchool()
        {
            ViewBag.sList = new SelectList(Enum.GetValues(typeof(States)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchool([Bind("ID,Address1,Address2,City,State,Name,Population")] Schools schools)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schools);
                await _context.SaveChangesAsync();
                return RedirectToAction("Schools");
            }
            return View(schools);
        }

        public async Task<IActionResult> EditSchool(int? id)
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
            ViewBag.sList = new SelectList(Enum.GetValues(typeof(States)));
            return View(schools);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchool(int id, [Bind("ID,Address1,Address2,City,State,Name,Population")] Schools schools)
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
                return RedirectToAction("Schools");
            }
            return View(schools);
        }

        public async Task<IActionResult> DeleteSchool(int? id)
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

        [HttpPost, ActionName("DeleteSchool")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSchoolConfirmed(int id)
        {
            var schools = await _context.Schools.SingleOrDefaultAsync(m => m.ID == id);
            _context.Schools.Remove(schools);
            await _context.SaveChangesAsync();
            return RedirectToAction("Schools");
        }

        private bool SchoolsExists(int id)
        {
            return _context.Schools.Any(e => e.ID == id);
        }
        #endregion

        #region Match Stuff

        public async Task<IActionResult> Matches(int? id)
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

        public IActionResult CreateMatch(int? tid)
        {
            if (tid != null)
            {
                ViewBag.tid = tid;
            }
            else
            {
                ViewBag.tid = -1;
            }

            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name");
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMatch(int? tid, [Bind("ID,MatchNumber,Team1ID, Team2ID, Winner, Datetime, TournamentID")] Matches matches)
        {

            if (ModelState.IsValid)
            {
                _context.Add(matches);
                await _context.SaveChangesAsync();

                if(tid == -1)
                {
                    return RedirectToAction("Matches");
                }
                else
                {
                    return RedirectToAction("Matches", new { id = tid });
                }
            }
            return View(matches);
        }

        public async Task<IActionResult> EditMatch(int? tid, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (tid != null)
            {
                ViewBag.tid = tid;
            }
            else
            {
                ViewBag.tid = -1;
            }
            var match = await _context.Matches.SingleOrDefaultAsync(m => m.ID == id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name");
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Name");
            return View(match);
        }

        [HttpPost]
        public async Task<IActionResult> EditMatch(int? tid, int id, [Bind("ID,MatchNumber,Team1ID, Team2ID, Winner, Datetime, TournamentID")] Matches matches)
        {
            if (id != matches.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentsExists(matches.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (tid == -1)
                {
                    return RedirectToAction("Matches");
                }
                else
                {
                    return RedirectToAction("Matches", new { id = tid });
                }
            }
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name");
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Name");
            return View(matches);
        }

        public async Task<IActionResult> DeleteMatch(int? id, int? tid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.Include(m => m.Team1).Include(m => m.Team2).Include(m => m.Tournament).SingleOrDefaultAsync(m => m.ID == id);
            if (match == null)
            {
                return NotFound();
            }

            if (tid != null)
            {
                ViewBag.tid = tid;
            }
            else
            {
                ViewBag.tid = -1;
            }

            return View(match);
        }

        [HttpPost, ActionName("DeleteMatch")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMatchConfirmed(int id, int? tid)
        {
            var match = await _context.Matches.SingleOrDefaultAsync(m => m.ID == id);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            if (tid == -1)
            {
                return RedirectToAction("Matches");
            }
            else
            {
                return RedirectToAction("Matches", new { id = tid });
            }
        }

        #endregion
    }
}