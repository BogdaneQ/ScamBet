using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScamBet.Models
{
    public class Coinflip
    {
        [Key]
        [Required]
        public int bet_ID_cf { get; set; }

        [ForeignKey("user_ID")]
        [Required]
        public int user_ID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The bet amount must be greater than 0")]
        public double BetAmount_cf { get; set; }

        [Required]
        public string Choice { get; set; } // "heads", "tails", "back"

        [Required]
        public DateTime BetTime_cf { get; set; }

        public bool IsWin_cf { get; set; }
    }
}
