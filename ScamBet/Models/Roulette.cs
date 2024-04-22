using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Roulette
    {
        [Key]
        [Required]
        public int rouletteID { get; set; }
        public double balance { get; set; }
        [Required] 
        public DateTime time { get; set; }
    }
}
