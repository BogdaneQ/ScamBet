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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {

            var user = _context.Accounts.Include(m => m.role_ID).Where(m => m.email == email).FirstOrDefault();
                if (user == null)
                {
                    return View();
                }

                if(user.password == password)
                {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Email, email));
                
                identity.AddClaim(new Claim(ClaimTypes.Role, user.role_ID.ToString()));

                var claims = identity;
                var principal = new ClaimsPrincipal(claims);
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
