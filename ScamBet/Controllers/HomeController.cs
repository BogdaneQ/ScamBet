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
using System.Web.Helpers;

namespace ScamBet.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly BookmacherDBContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(BookmacherDBContext context, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
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
                ViewBag.ErrorMessage = "Nieprawid這wy email lub konto zosta這 zbanowane.";
                return View();
            }

            var Salt = _configuration.GetSection("salt").Value;
            string HashAndSalt = string.Concat(password, Salt);

            if (Crypto.VerifyHashedPassword(user.password,HashAndSalt))
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
                ViewBag.ErrorMessage = "Nieprawid這we has這.";
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
                var Salt = _configuration.GetSection("salt").Value;
                string HashAndSalt = string.Concat(account.password, Salt);

                string FinalPassword = Crypto.HashPassword(HashAndSalt);
                account.password = FinalPassword;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(double amount)
        {
            if (amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "Deposit amount must be greater than zero.");
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _context.Accounts.FindAsync(int.Parse(userId));
            if (user != null)
            {
                user.acc_balance += amount;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserIndex));
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(double amount)
        {
            if (amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "Withdraw amount must be greater than zero.");
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _context.Accounts.FindAsync(int.Parse(userId));
            if (user != null)
            {
                if (user.acc_balance >= amount)
                {
                    user.acc_balance -= amount;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(UserIndex));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Insufficient funds.");
                    return View();
                }
            }

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
