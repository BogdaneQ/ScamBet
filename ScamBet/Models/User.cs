using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entitties
{
    public class User : Account
    {

        [Required]
        [Range(0, double.MaxValue)] 
        public double acc_balace { get; set; } = 0;
    }
}
