using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;

namespace ScamBet.Controllers
{
    public class MatchController : Controller
    {
        private readonly BookmacherDBContext _context;

        public MatchController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Match
        public async Task<IActionResult> Index()
        {
            return View(await _context.matches.ToListAsync());
        }

        // GET: Match/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }


        // GET: Match/Create
        public IActionResult Create()
        {
            ViewData["Team1"] = new SelectList(_context.teams, "team_ID", "name");
            ViewData["Team2"] = new SelectList(_context.teams, "team_ID", "name");
            return View();
        }

        // POST: Match/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("match_ID,team1_ID,team2_ID,time,team1_goals,team2_goals,isPlayed,winner_ID")] Match match)
        {
            if (ModelState.IsValid)
            {
                AssignWinner(match);
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team1"] = new SelectList(_context.teams, "team_ID", "name", match.team1_ID);
            ViewData["Team2"] = new SelectList(_context.teams, "team_ID", "name", match.team2_ID);
            return View(match);
        }

        // GET: Match/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["Team1"] = new SelectList(_context.teams, "team_ID", "name", match.team1_ID);
            ViewData["Team2"] = new SelectList(_context.teams, "team_ID", "name", match.team2_ID);
            return View(match);
        }

        // POST: Match/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("match_ID,team1_ID,team2_ID,time,team1_goals,team2_goals,isPlayed,winner_ID")] Match match)
        {
            if (id != match.match_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AssignWinner(match);
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.match_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team1"] = new SelectList(_context.teams, "team_ID", "name", match.team1_ID);
            ViewData["Team2"] = new SelectList(_context.teams, "team_ID", "name", match.team2_ID);
            return View(match);
        }

        // GET: Match/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.matches
                .FirstOrDefaultAsync(m => m.match_ID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.matches.FindAsync(id);
            _context.matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.matches.Any(e => e.match_ID == id);
        }

        private void AssignWinner(Match match)
        {
            if (match.team1_goals > match.team2_goals)
            {
                match.winner_ID = match.team1_ID;
            }
            else if (match.team2_goals > match.team1_goals)
            {
                match.winner_ID = match.team2_ID;
            }
            else
            {
                match.winner_ID = 0; // 0 represents a draw or no winner
            }
        }
    }
}
