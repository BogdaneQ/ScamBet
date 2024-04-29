using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;
using System.Linq;

namespace ScamBet.Controllers
{
    public class MatchController : Controller
    {
        private readonly BookmacherDBContext _context;

        public MatchController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Match/Index
        public IActionResult Index()
        {
            var matches = _context.matches.ToList();
            return View(matches);
        }

        // GET: Match/Create
        public IActionResult Create()
        {
            var teams = _context.teams.ToList();
            ViewData["HomeTeamId"] = new SelectList(teams, "Id", "Name");
            ViewData["AwayTeamId"] = new SelectList(teams, "Id", "Name");
            return View();
        }

        // POST: Match/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                _context.matches.Add(match);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        // GET: Match/Edit/5
        public IActionResult Edit(int id)
        {
            var match = _context.matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        // POST: Match/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Match match)
        {
            if (id != match.match_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(match).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        // GET: Match/Delete/5
        public IActionResult Delete(int id)
        {
            var match = _context.matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var match = _context.matches.Find(id);
            _context.matches.Remove(match);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
