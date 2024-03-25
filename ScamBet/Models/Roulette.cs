using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entitties
{
    public class Roulette
    {
        [Key]
        [Required]
        public int rouletteID { get; set; }

        [ForeignKey("Account")]
        [Required]
        public int user_ID { get; set; }
        public double balance { get; set; }
        [Required] 
        public DateTime time { get; set; }
    }
}
