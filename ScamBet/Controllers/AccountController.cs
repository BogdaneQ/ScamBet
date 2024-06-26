﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;
using System.IO;
using ScamBet.Models;

namespace ScamBet.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly BookmacherDBContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(BookmacherDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Account
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Account> accounts = _context.Accounts;

            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a => a.username.Contains(searchString));
            }

            return View(await accounts.ToListAsync());
        }

        // GET: Account/MyAccount
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }

            int id = int.Parse(userId);

            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.user_ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Account/Details
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
                var Salt = _configuration.GetSection("salt").Value;
                string HashAndSalt = string.Concat(account.password, Salt);
                string FinalPassword = Crypto.HashPassword(HashAndSalt);
                account.password = FinalPassword;
                account.acc_balance = 0;
                account.isBanned = false;
                account.role_ID = 1;
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Account/Edit
        [Authorize(Roles = "Admin")]
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
                new { Value = 1, Text = "User" },
                new { Value = 2, Text = "Admin" }
            }, "Value", "Text", account.role_ID);

            return View(account);
        }

        // POST: Account/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    var existingAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.user_ID == id);
                    if (existingAccount == null)
                    {
                        return NotFound();
                    }
                    
                    if (account.AvatarPath == null)
                    {
                        account.AvatarPath = existingAccount.AvatarPath;
                    }

                    if (string.IsNullOrEmpty(account.password))
                    {
                        account.password = existingAccount.password;
                    }
                    else
                    {
                        var Salt = _configuration.GetSection("salt").Value;
                        string HashAndSalt = string.Concat(account.password, Salt);
                        string FinalPassword = Crypto.HashPassword(HashAndSalt);
                        account.password = FinalPassword;
                    }

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
                new { Value = 1, Text = "User" },
                new { Value = 2, Text = "Admin" }
            }, "Value", "Text", account.role_ID);

            return View(account);
        }

        // GET: Account/EditProfile
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditProfile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var account = await _context.Accounts.FindAsync(userId);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditProfile([Bind("user_ID,username,name,surname,password,email,phone_number")] Account account, IFormFile avatar)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId != account.user_ID)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.user_ID == userId);
                    if (existingAccount == null)
                    {
                        return NotFound();
                    }

                    if (avatar == null || avatar.Length == 0)
                    {
                        account.AvatarPath = existingAccount.AvatarPath;
                    }
                    else
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(avatar.FileName)}";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await avatar.CopyToAsync(stream);
                        }

                        account.AvatarPath = $"/images/{fileName}";
                    }

                    account.acc_balance = existingAccount.acc_balance;
                    account.TotalWinnings = existingAccount.TotalWinnings;
                    account.role_ID = existingAccount.role_ID;

                    if (string.IsNullOrEmpty(account.password))
                    {
                        account.password = existingAccount.password;
                    }
                    else
                    {
                        var Salt = _configuration.GetSection("salt").Value;
                        string HashAndSalt = string.Concat(account.password, Salt);
                        string FinalPassword = Crypto.HashPassword(HashAndSalt);
                        account.password = FinalPassword;
                    }

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
                return RedirectToAction(nameof(MyAccount));
            }
            return View(account);
        }


        // GET: Account/Delete
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DownloadProfilePicture(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null || string.IsNullOrEmpty(account.AvatarPath))
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", account.AvatarPath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var fileName = Path.GetFileName(filePath);
            return File(memory, "application/octet-stream", fileName);
        }

        // GET: Account/TotalWinnings
        public async Task<IActionResult> TotalWinnings(string searchString)
        {
            var query = _context.Accounts
                .Where(a => a.role_ID != 2)
                .Select(a => new AccountViewModel
                {
                    user_ID = a.user_ID,
                    username = a.username,
                    acc_balance = a.acc_balance,
                    TotalWinnings = a.TotalWinnings,
                    TotalDeposits = _context.Transactions
                        .Where(t => t.AccountId == a.user_ID && t.Type == TransactionType.Deposit)
                        .Sum(t => t.Amount),
                    TotalWithdrawals = _context.Transactions
                        .Where(t => t.AccountId == a.user_ID && t.Type == TransactionType.Withdrawal)
                        .Sum(t => t.Amount),
                    Balance = a.acc_balance
                });

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.username.Contains(searchString));
            }

            var accountsWithWinnings = await query.OrderByDescending(a => a.TotalWinnings).ToListAsync();

            accountsWithWinnings.ForEach(a =>
            {
                a.Balance = a.acc_balance + a.TotalWithdrawals - a.TotalDeposits;
            });

            return View(accountsWithWinnings);
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.user_ID == id);
        }
    }
}
