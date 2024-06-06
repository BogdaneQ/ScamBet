using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScamBet.Controllers
{
    [Authorize]
    public class RouletteController : Controller
    {
        private readonly BookmacherDBContext _context;

        public RouletteController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Roulette/Play
        public IActionResult Play()
        {
            return View();
        }

        // POST: Roulette/PlaceBet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceBet(string betType, string betValue, double betAmount)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var account = await _context.Accounts.FindAsync(userId);

            if (account == null || betAmount <= 0 || account.acc_balance < betAmount)
            {
                return BadRequest("Invalid bet amount or insufficient balance.");
            }

            var rouletteResult = GenerateRouletteResult();
            bool isWin = DetermineWin(betType, betValue, rouletteResult);

            double winnings = 0;
            if (isWin)
            {
                winnings = betAmount * (betType == "number" ? 35 : 2);
                account.acc_balance += winnings;
            }
            else
            {
                account.acc_balance -= betAmount;
            }

            var bet = new Roulette
            {
                user_ID = userId,
                betType_r = betType,
                betValue_r = betValue,
                betAmount_r = betAmount,
                betTime_r = DateTime.Now,
                isWin_r = isWin
            };

            _context.Roulette.Add(bet);
            await _context.SaveChangesAsync();

            ViewBag.RouletteResult = rouletteResult;
            ViewBag.IsWin = isWin;
            ViewBag.Winnings = winnings;
            ViewBag.Balance = account.acc_balance;
            ViewBag.BetValue = betValue;

            return View("Play");
        }

        private string GenerateRouletteResult()
        {
            var random = new Random();
            int number = random.Next(0, 37);
            string color = number == 0 ? "green" : (number % 2 == 0 ? "red" : "black");
            return $"{number}:{color}";
        }

        private bool DetermineWin(string betType, string betValue, string rouletteResult)
        {
            var resultParts = rouletteResult.Split(':');
            var number = resultParts[0];
            var color = resultParts[1];

            if (betType == "color" && betValue == color)
            {
                return true;
            }

            if (betType == "number" && betValue == number)
            {
                return true;
            }

            return false;
        }
    }
}
