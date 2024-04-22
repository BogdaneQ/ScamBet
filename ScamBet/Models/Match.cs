using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Match
    {
        [Key]
        [Required] 
        public int matchID { get; set; }
        
        [ForeignKey("Team_results")] 
        public int trID { get; set; }
        public Team_results Team_results { get; set; }

        [Required] 
        public DateTime time {  get; set; }
        
    }
}
