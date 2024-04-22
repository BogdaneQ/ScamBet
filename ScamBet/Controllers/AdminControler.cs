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
    public class AdminController : Controller
    {
        private readonly BookmacherDBContext _context;

        public AdminController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Index
        public IActionResult Index()
        {
            var admins = _context.admins;
            return View(admins);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            var AllAccounts = _context.accounts.ToList();
            ViewData["AccountList"] = new SelectList (AllAccounts, "username", "username");
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Admin admin)
        {
            var Account = _context.accounts.FirstOrDefault(a => a.username == admin.username);
            admin.name = Account.name;
            admin.surname = Account.surname;
            admin.phone_number = Account.phone_number;
            admin.email = Account.email;
            admin.password = Account.password;
            admin.isBanned = Account.isBanned;

            
                _context.accounts.Remove(Account);
                _context.admins.Add(admin);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
            return View(admin);
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int id)
        {
            var admin = _context.admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Admin admin)
        {
            if (id != admin.user_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.admins.Update(admin);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public IActionResult Delete(int id)
        {
            var admin = _context.admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var admin = _context.admins.Find(id);
            _context.admins.Remove(admin);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
