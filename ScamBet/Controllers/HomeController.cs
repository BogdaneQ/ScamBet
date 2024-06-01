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
