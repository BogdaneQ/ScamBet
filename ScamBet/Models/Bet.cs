using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Bet
    {
        [Key]
        [Required]
        public int bet_ID { get; set; }

        [ForeignKey("Account")]
        [Required]
        public int user_ID { get; set; }
        public virtual Account bet_placer { get; set; }

        [Required]
        [StringLength(50)]
        public string succes { get; set; }

        [ForeignKey("Match")]
        [Required]
        public int match_ID { get; set; }
        public virtual Match match { get; set; }

        [Required]
        public double ratio { get; set; }

        [Required]
        public double value { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public bool active { get; set; } = true;
    }
}