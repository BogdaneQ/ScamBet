using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Bet
    {
        [Key]
        [Required] 
        public int bet_ID {  get; set; }

        [ForeignKey("Account")]
        [Required]
        public int user_ID { get; set; }

        [Required]
        [Range(1, 2)]
        public int choosen_team {  get; set; }

        [ForeignKey("Match")]
        [Required]
        public int match_ID { get; set; }

        [Required]
        public double ratio { get; set; }

        [Required]
        public double value { get; set; }

        [Required]
        public bool active { get; set; } = true;
    }
}
