using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;
using ScamBet.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ScamBet.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly BookmacherDBContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(BookmacherDBContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
      
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult UserIndex()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {

            var user = _context.Accounts.Include(a => a.Role).FirstOrDefault(m => m.email == email);
            if (user == null || user.isBanned)
                {
                ViewBag.ErrorMessage = "Nieprawidłowy email lub konto zostało zbanowane.";
                return View();
                }

                if(user.password == password)
                {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.user_ID.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, email));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.RoleName));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (user.Role.RoleName == RoleType.Admin.ToString())
                    {
                        return RedirectToAction("AdminIndex", "Home");
                    }
                else
                    {
                        return RedirectToAction("UserIndex", "Home");
                    }
                }

                else
                {
                ViewBag.ErrorMessage = "Nieprawidłowe hasło.";
                return View();
                }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("username,name,surname,password,email,phone_number")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.acc_balance = 0;
                account.isBanned = false;
                account.role_ID = 1;
                _context.Add(account);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }
            return View(account);
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(double amount)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.email == User.Identity.Name);

            if (user != null)
            {
                user.acc_balance += amount;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(UserIndex));
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(double amount)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.email == User.Identity.Name);

            if (user != null)
            {
                if (user.acc_balance >= amount)
                {
                    user.acc_balance -= amount;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Insufficient funds.");
                    return View();
                }
            }

            return RedirectToAction(nameof(UserIndex));
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

        }
    }
}
