using Microsoft.AspNetCore.Mvc;

namespace ScamBet.Controllers
{
    public class LiveGameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
