using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; // Assuming Identity is used for user management
using ScamBet.Models; // Assuming LoginViewModel is defined here
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;

public class AccessController : Controller
{
    private readonly UserManager<IdentityUser> _userManager; // Assuming UserManager for user management

    public AccessController(UserManager<IdentityUser> userManager) // Dependency injection
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        ClaimsPrincipal User = HttpContext.User;

        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) // Validate user input before database interaction
        {
            return View(model);
        }

        // Authenticate with Identity (assuming Identity is used)
        IdentityUser user = await _userManager.FindByNameAsync(model.Username);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.RememberMe
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);

            return RedirectToAction("Index", "Home");
        }

        ViewData["ValidateMessage"] = "Invalid username or password.";
        return View(model);
    }
}
