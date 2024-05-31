using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;
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
        public IActionResult Index()
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

            var roulette = await _context.roulettes.FirstOrDefaultAsync(m => m.roulette_ID == id);
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
        public async Task<IActionResult> Create([Bind("roulette_ID,name,difficulty")] Roulette roulette)
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
        public async Task<IActionResult> Edit(int id, [Bind("roulette_ID,name,difficulty")] Roulette roulette)
        {
            if (id != roulette.roulette_ID)
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
                    if (!RouletteExists(roulette.roulette_ID))
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

            var roulette = await _context.roulettes.FirstOrDefaultAsync(m => m.roulette_ID == id);
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
            return _context.roulettes.Any(e => e.roulette_ID == id);
        }
    }
}
