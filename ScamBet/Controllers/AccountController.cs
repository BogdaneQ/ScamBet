using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;

namespace ScamBet.Controllers
{
    
    
    public class AccountController : Controller
    {
        private readonly BookmacherDBContext _context;

        public AccountController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounts.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.user_ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("user_ID,username,name,surname,password,email,phone_number")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.acc_balance = 0;
                account.isBanned = false;
                account.role_ID = 0;
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            // Ustawienie możliwych opcji roli do wyboru
            ViewData["RoleOptions"] = new SelectList(new[]
            {
                new { Value = 0, Text = "User" },
                new { Value = 1, Text = "Admin" }
            }, "Value", "Text", account.role_ID);

            return View(account);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("user_ID,username,name,surname,password,email,phone_number,isBanned,acc_balance,role_ID")] Account account)
        {
            if (id != account.user_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.user_ID))
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

            // Ustawienie możliwych opcji roli do wyboru
            ViewData["RoleOptions"] = new SelectList(new[]
            {
                new { Value = 0, Text = "User" },
                new { Value = 1, Text = "Admin" }
            }, "Value", "Text", account.role_ID);

            return View(account);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.user_ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.user_ID == id);
        }
    }
}
