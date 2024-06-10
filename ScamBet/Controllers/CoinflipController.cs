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
    public class CoinflipController : Controller
    {
        private readonly BookmacherDBContext _context;

        public CoinflipController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Coinflip/Play
        public async Task<IActionResult> Play()
        {
            var recentBets = await _context.Coinflip
                                           .OrderByDescending(b => b.BetTime_cf)
                                           .Take(5)
                                           .ToListAsync();

            ViewBag.RecentBets = recentBets ?? new List<Coinflip>();

            return View();
        }

        // POST: Coinflip/PlaceBet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceBet(string choice, double betAmount)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var account = await _context.Accounts.FindAsync(userId);

            if (account == null || betAmount <= 0 || account.acc_balance < betAmount)
            {
                return BadRequest("Invalid bet amount or insufficient balance.");
            }


            double winnings = 0;
            bool isWin = false;
            string resultImage = string.Empty;

            var randomNumber = new Random().Next(0, 100);

            if (randomNumber >= 1 && randomNumber < 48 && choice == "heads")
            {
                isWin = true;
                resultImage = "heads.png";
            }
            else if (randomNumber >= 48 && randomNumber < 96 && choice == "tails") 
            {
                isWin = true;
                resultImage = "tails.png";
            }
            else if (randomNumber >= 96 && choice == "back") 
            {
                isWin = true;
                resultImage = "back.png";
            }

            // Update account balance and TotalWinnings
            if (isWin)
            {
                winnings = (betAmount * GetMultiplier(choice)) - betAmount;
                account.acc_balance += winnings;
                account.TotalWinnings += winnings;
            }
            else
            {
                account.acc_balance -= betAmount;

                if (choice == "back")
                {
                    resultImage = randomNumber < 48 ? "heads.png" : "tails.png";
                }
                else
                {
                    resultImage = choice == "heads" ? "tails.png" : "heads.png";
                }
            }

            // Save the bet details
            var bet = new Coinflip
            {
                user_ID = userId,
                BetAmount_cf = betAmount,
                Choice = choice,
                IsWin_cf = isWin,
                BetTime_cf = DateTime.Now
            };

            _context.Coinflip.Add(bet);
            await _context.SaveChangesAsync();

            // Prepare view data
            ViewBag.IsWin = isWin;
            ViewBag.Winnings = winnings;
            ViewBag.Balance = account.acc_balance;
            ViewBag.BetValue = choice;
            ViewBag.BetAmount = betAmount;
            ViewBag.ResultImage = resultImage;

            var recentBets = await _context.Coinflip
                                           .OrderByDescending(b => b.BetTime_cf)
                                           .Take(5)
                                           .ToListAsync();
            ViewBag.RecentBets = recentBets ?? new List<Coinflip>();

            return View("Play");
        }

        // POST: Coinflip/RepeatBet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RepeatBet()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var lastBet = await _context.Coinflip
                                        .Where(b => b.user_ID == userId)
                                        .OrderByDescending(b => b.BetTime_cf)
                                        .FirstOrDefaultAsync();

            if (lastBet == null)
            {
                return BadRequest("No previous bet to repeat.");
            }

            return await PlaceBet(lastBet.Choice, lastBet.BetAmount_cf);
        }

        private double GetMultiplier(string choice)
        {
            return choice switch
            {
                "heads" => 2,
                "tails" => 2,
                "back" => 25,
                _ => 1
            };
        }
    }
}
