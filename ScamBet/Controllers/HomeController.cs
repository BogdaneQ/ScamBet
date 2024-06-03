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

        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult Privacy()
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
                    return View();
                }

                if(user.password == password)
                {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Email, email));
                
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.RoleName));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    return View();
                }
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

        }
    }
}
