using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Team_results
    {
        [Key]
        [Required] 
        public int trID {  get; set; }

        [ForeignKey("Team")]
        [Required] 
        public int TeamID { get; set; }
        public Team Team { get; set; }

        [Required] 
        public bool winner { get; set; }

        [Required] 
        public int goals { get; set; }

        [Required] 
        public int fouls { get; set; }

        [Required] 
        public int red_cards { get; set; }

        [Required] 
        public int yellow_cards { get; set; }

        [Required] 
        public int shots { get; set; }

        [Required] 
        public int shots_ontarget { get; set; }

        [Required] 
        public int corners { get; set; }
    }
}
