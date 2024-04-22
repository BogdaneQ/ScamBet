using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Admin : Account
    {

        [Required]
        [Range(1, 3)]
        public int? admin_rank {  get; set; }
    }
}
