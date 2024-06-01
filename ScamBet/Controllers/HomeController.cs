using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ScamBet.Models;
using ScamBet.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Find the user by username
        var user = _context.accounts.Include(m => m.role_ID).FirstOrDefault(m => m.username == model.Username);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // Validate the password (assuming saltedPassword is hashed with salt)
        var saltedPassword = HashPasswordWithSalt(model.Password, user.email);
        if (user.password != saltedPassword)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        _logger.LogInformation("User logged in.");
        return View();
    }

    private string HashPasswordWithSalt(string password, string salt)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var saltedPassword = $"{password}{salt}";
            var saltedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
            var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            return System.Convert.ToBase64String(hashBytes);
        }
    }
}
