using System.ComponentModel.DataAnnotations;
namespace ScamBet.Entitties
{
    public class Team
    {
        [Key]
        [Required] 
        public int teamID { get; set; }

        [Required]
        [StringLength(50)] 
        public string name { get; set; }

        [Required]
        [Range(0, 100)]
        public int attack { get; set; }

        [Required]
        [Range(0, 100)] 
        public int middle { get; set; }

        [Required]
        [Range(0, 100)] 
        public int defence { get; set; }

        [Required]
        [Range(0, 100)] 
        public int aggresion { get; set; }
    }
}
