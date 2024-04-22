using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;

namespace ScamBet.Controllers
{
    public class TeamResultsController : Controller
    {
        private readonly BookmacherDBContext _context;

        public TeamResultsController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: TeamResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.teams_results.ToListAsync());
        }

        // GET: TeamResults/Create
        public IActionResult Create()
        {
            var AllTeams = _context.teams.ToList();
            ViewData["TeamList"] = new SelectList(AllTeams, "name", "name");
            return View();
        }

        // POST: TeamResults/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("trID,TeamID,winner,goals,fouls,red_cards,yellow_cards,shots,shots_ontarget,corners")] Team_results team_results)
        {
            var Team = _context.teams.FirstOrDefault(a => a.name == team_results.Team.name);

            Team.name = team_results.Team.name;

                _context.Add(team_results);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(team_results);
        }

        // GET: TeamResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team_results = await _context.teams_results.FindAsync(id);
            if (team_results == null)
            {
                return NotFound();
            }
            return View(team_results);
        }

        // POST: TeamResults/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("trID,TeamID,winner,goals,fouls,red_cards,yellow_cards,shots,shots_ontarget,corners")] Team_results team_results)
        {
            if (id != team_results.trID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team_results);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Team_resultsExists(team_results.trID))
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
            return View(team_results);
        }

        // GET: TeamResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team_results = await _context.teams_results
                .FirstOrDefaultAsync(m => m.trID == id);
            if (team_results == null)
            {
                return NotFound();
            }

            return View(team_results);
        }

        // POST: TeamResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team_results = await _context.teams_results.FindAsync(id);
            _context.teams_results.Remove(team_results);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Team_resultsExists(int id)
        {
            return _context.teams_results.Any(e => e.trID == id);
        }
    }
}
