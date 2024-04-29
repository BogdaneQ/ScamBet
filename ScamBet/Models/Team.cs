using System.ComponentModel.DataAnnotations;
namespace ScamBet.Entities
{
    public class Team
    {
        [Key]
        [Required] 
        public int team_ID { get; set; }

        [Required]
        [StringLength(50)] 
        public string name { get; set; }

        [Required]
        public int wins { get; set; }

        [Required]
        public int draws { get; set; }

        [Required]
        public int loses { get; set; }

        [Required]
        public int points { get; set; }

    }
}
