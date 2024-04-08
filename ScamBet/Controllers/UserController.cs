using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entitties;
using ScamBet.Entitties;

namespace ScamBet.Controllers
{
    public class UserController : Controller
    {
        private readonly BookmacherDBContext _context;

        public UserController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: User/Index
        public IActionResult Index()
        {
            var users = _context.users;
            return View(users);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            var allAccounts = _context.accounts.ToList();
            ViewData["AccountList"] = new SelectList(allAccounts, "Username", "Username");
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            var account = _context.accounts.FirstOrDefault(a => a.Username == user.Username);
            if (account != null)
            {
                user.name = account.name;
                user.Surname = account.Surname;
                user.phone_number = account.phone_number;
                user.email = account.email;
                user.password = account.password;
                user.isBanned = account.isBanned;

                _context.accounts.Remove(account);
                _context.users.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public IActionResult Edit(int id)
        {
            var user = _context.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (id != user.user_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.users.Update(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public IActionResult Delete(int id)
        {
            var user = _context.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.users.Find(id);
            _context.users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
