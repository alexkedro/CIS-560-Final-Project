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

        public IActionResult TournamentDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Matches", new { id = id });
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

        public IActionResult SchoolDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Teams", new { id = id });
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
            ViewData["TournamentID"] = new SelectList(_context.Tournaments, "ID", "Name");
            ViewData["TeamsID"] = new SelectList(_context.Teams, "ID", "Name");
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

        #region Team Stuff

        public async Task<IActionResult> Teams(int? id)
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

        public IActionResult CreateTeam(int? tid)
        {
            if (tid != null)
            {
                ViewBag.tid = tid;
            }
            else
            {
                ViewBag.tid = -1;
            }

            ViewData["SchoolID"] = new SelectList(_context.Schools, "ID", "Name");
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeam(int? tid, [Bind("ID,SchoolID,GameID, Name")] Teams teams)
        {

            if (ModelState.IsValid)
            {
                _context.Add(teams);
                await _context.SaveChangesAsync();

                if (tid == -1)
                {
                    return RedirectToAction("Teams");
                }
                else
                {
                    return RedirectToAction("Teams", new { id = tid });
                }
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "ID", "Name");
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "name");
            return View(teams);
        }

        public async Task<IActionResult> EditTeam(int? tid, int? id)
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
            var team = await _context.Teams.SingleOrDefaultAsync(t => t.ID == id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "ID", "Name");
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "name");
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> EditTeam(int? tid, int id, [Bind("ID,SchoolID,GameID, Name")] Teams teams)
        {
            if (id != teams.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentsExists(teams.ID))
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
                    return RedirectToAction("Teams");
                }
                else
                {
                    return RedirectToAction("Teams", new { id = tid });
                }
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "ID", "Name");
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "name");
            return View(teams);
        }

        public async Task<IActionResult> DeleteTeam(int? id, int? tid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.Include(t => t.School).Include(t => t.Game).SingleOrDefaultAsync(t => t.ID == id);
            if (team == null)
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

            return View(team);
        }

        [HttpPost, ActionName("DeleteTeam")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeamConfirmed(int id, int? tid)
        {
            var team = await _context.Teams.SingleOrDefaultAsync(t => t.ID == id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            if (tid == -1)
            {
                return RedirectToAction("Teams");
            }
            else
            {
                return RedirectToAction("Teams", new { id = tid });
            }
        }

        public IActionResult TeamDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Members", new { id = id });
        }

        #endregion

        #region Member Stuff

        public async Task<IActionResult> Members(int? id)
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
                foreach(TeamsMembers tm in members)
                {
                    var coach = await _context.Coaches.SingleOrDefaultAsync(i => i.ID == tm.ID);
                    var player = await _context.Players.SingleOrDefaultAsync(i => i.ID == tm.ID);

                    if(coach != null)
                    {
                        teammems.Add(new PlayerViewModel
                        {
                            ID = coach.ID,
                            FirstName = coach.FirstName,
                            LastName = coach.LastName,
                            cop = CoachOrPlayer.Coach
                        });
                    }
                    else if(player != null)
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

        #endregion

        #region Scrim Stuff

        public async Task<IActionResult> Scrims()
        {
            var siteContext = _context.Scrims.Include(s => s.Team1).Include(s => s.Team2);
            return View(await siteContext.ToListAsync());
        }

        public IActionResult CreateScrim()
        {
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Name");
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Team1ID,Team2ID,Winner,Datetime")] Scrims scrims)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scrims);
                await _context.SaveChangesAsync();
                return RedirectToAction("Scrims");
            }
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Name", scrims.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Name", scrims.Team2ID);
            return View(scrims);
        }

        public async Task<IActionResult> EditScrim(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrims = await _context.Scrims.SingleOrDefaultAsync(m => m.ID == id);
            if (scrims == null)
            {
                return NotFound();
            }
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Name", scrims.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Name", scrims.Team2ID);
            return View(scrims);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditScrim(int id, [Bind("ID,Team1ID,Team2ID,Winner,Datetime")] Scrims scrims)
        {
            if (id != scrims.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scrims);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScrimsExists(scrims.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Scrims");
            }
            ViewData["Team1ID"] = new SelectList(_context.Teams, "ID", "Name", scrims.Team1ID);
            ViewData["Team2ID"] = new SelectList(_context.Teams, "ID", "Name", scrims.Team2ID);
            return View(scrims);
        }

        public async Task<IActionResult> DeleteScrim(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrims = await _context.Scrims
                .Include(s => s.Team1)
                .Include(s => s.Team2)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (scrims == null)
            {
                return NotFound();
            }

            return View(scrims);
        }

        [HttpPost, ActionName("DeleteScrim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteScrimConfirmed(int id)
        {
            var scrims = await _context.Scrims.SingleOrDefaultAsync(m => m.ID == id);
            _context.Scrims.Remove(scrims);
            await _context.SaveChangesAsync();
            return RedirectToAction("Scrims");
        }

        private bool ScrimsExists(int id)
        {
            return _context.Scrims.Any(e => e.ID == id);
        }
        #endregion
    }
}