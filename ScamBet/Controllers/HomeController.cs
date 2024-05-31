using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ScamBet.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
}
