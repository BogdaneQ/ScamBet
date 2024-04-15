using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entitties;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ScamBet.Controllers
{
    public class RouletteController : Controller
    {
        private readonly BookmacherDBContext _context;

        public RouletteController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Roulette/Index
        public async Task<IActionResult> Index()
        {
            var roulettes = _context.roulettes.ToList();
            return View(roulettes);
        }

        // GET: Roulette/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roulette = await _context.roulettes.FirstOrDefaultAsync(m => m.rouletteID == id);
            if (roulette == null)
            {
                return NotFound();
            }

            return View(roulette);
        }

        // GET: Roulette/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roulette/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("rouletteID,user_ID,balance,time")] Roulette roulette)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roulette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roulette);
        }

        // GET: Roulette/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roulette = await _context.roulettes.FindAsync(id);
            if (roulette == null)
            {
                return NotFound();
            }
            return View(roulette);
        }

        // POST: Roulette/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("rouletteID,user_ID,balance,time")] Roulette roulette)
        {
            if (id != roulette.rouletteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roulette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouletteExists(roulette.rouletteID))
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
            return View(roulette);
        }

        // GET: Roulette/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roulette = await _context.roulettes.FirstOrDefaultAsync(m => m.rouletteID == id);
            if (roulette == null)
            {
                return NotFound();
            }

            return View(roulette);
        }

        // POST: Roulette/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roulette = await _context.roulettes.FindAsync(id);
            _context.roulettes.Remove(roulette);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouletteExists(int id)
        {
            return _context.roulettes.Any(e => e.rouletteID == id);
        }
    }
}
