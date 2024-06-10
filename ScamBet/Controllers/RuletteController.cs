using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScamBet.Entities;
using ScamBet.Models;
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

        // GET: Roulette/Index
        
            public async Task<IActionResult> Index()
            {
                return View();
            }
        

        // GET: Roulette/Play
        public async Task<IActionResult> Play()
        {
            var recentResults = await _context.Roulette
                                           .OrderByDescending(b => b.betTime_r)
                                           .Take(10)
                                           .ToListAsync();
            ViewBag.RecentBets = recentResults;
            return View();
        }

        //POST
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
                winnings = (betAmount * GetMultiplier(betType));
                account.acc_balance -= betAmount;
                account.acc_balance += winnings;
                account.TotalWinnings += winnings - betAmount;
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
                isWin_r = isWin,
                result_r = rouletteResult // Added result_r property
            };

            _context.Roulette.Add(bet);
            await _context.SaveChangesAsync();

            ViewBag.RouletteResult = rouletteResult;
            ViewBag.IsWin = isWin;
            ViewBag.Winnings = winnings;
            ViewBag.Balance = account.acc_balance;
            ViewBag.BetValue = betValue;
            ViewBag.BetAmount = betAmount; // Add bet amount to ViewBag

            var recentResults = await _context.Roulette
                                           .OrderByDescending(b => b.betTime_r)
                                           .Take(10)
                                           .ToListAsync();
            ViewBag.RecentBets = recentResults;

            return View("Play");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RepeatBet()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var lastBet = await _context.Roulette
                                        .Where(b => b.user_ID == userId)
                                        .OrderByDescending(b => b.betTime_r)
                                        .FirstOrDefaultAsync();

            if (lastBet == null)
            {
                return BadRequest("No previous bet to repeat.");
            }

            return await PlaceBet(lastBet.betType_r, lastBet.betValue_r, lastBet.betAmount_r);
        }

        private string GenerateRouletteResult()
        {
            var random = new Random();
            int number = random.Next(0, 37);
            string color = "";

            // Przypisz kolor na podstawie numeru
            switch (number)
            {
                case 0:
                    color = "green";
                    break;
                case int n when (n >= 1 && n <= 10) || (n >= 19 && n <= 28):
                    color = n % 2 == 0 ? "black" : "red";
                    break;
                case int n when (n >= 11 && n <= 18) || (n >= 29 && n <= 36):
                    color = n % 2 == 0 ? "red" : "black";
                    break;
            }

            return $"{number}:{color}";
        }

        private bool DetermineWin(string betType, string betValue, string rouletteResult)
        {
            var resultParts = rouletteResult.Split(':');
            var number = int.Parse(resultParts[0]);
            var color = resultParts[1];

            if (betType == "color" && betValue == color)
            {
                return true;
            }

            if (betType == "number" && betValue == number.ToString())
            {
                return true;
            }

            if (betType == "odd_even" && betValue == (number % 2 == 0 ? "even" : "odd"))
            {
                return true;
            }

            if (betType == "dozen" && ((betValue == "1st" && number >= 1 && number <= 12) ||
                                       (betValue == "2nd" && number >= 13 && number <= 24) ||
                                       (betValue == "3rd" && number >= 25 && number <= 36)))
            {
                return true;
            }

            if (betType == "half" && ((betValue == "1-18" && number >= 1 && number <= 18) ||
                                      (betValue == "19-36" && number >= 19 && number <= 36)))
            {
                return true;
            }

            if (betType == "row" && ((betValue == "1st" && number % 3 == 1) ||
                                     (betValue == "2nd" && number % 3 == 2) ||
                                     (betValue == "3rd" && number % 3 == 0)))
            {
                return true;
            }

            return false;
        }

        private double GetMultiplier(string betType)
        {
            return betType switch
            {
                "number" => 35,
                "color" => 2,
                "odd_even" => 2,
                "dozen" => 3,
                "half" => 2,
                "row" => 3,
                _ => 1
            };
        }
        
    }
}
