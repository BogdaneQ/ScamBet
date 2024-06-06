using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Roulette
    {
        [Key]
        [Required]
        public int bet_ID_r { get; set; }

        [ForeignKey("user_ID")]
        [Required]
        public int user_ID { get; set; }

        [Required]
        public string betType_r { get; set; } // "color" or "number"

        [Required]
        public string betValue_r { get; set; } // "red", "black", or number as string

        [Required(ErrorMessage = "Place amount")]
        public decimal betAmount_r { get; set; }

        [Required]
        public DateTime betTime_r { get; set; }

        public bool isWin_r { get; set; }
    }
}
