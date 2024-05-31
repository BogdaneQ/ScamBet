using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScamBet.Entities
{
    public class Match
    {
        [Key]
        [Required]
        public int match_ID { get; set; }

        [ForeignKey("team1")]
        [Required]
        public int team1_ID { get; set; }
        public virtual Team Team1 { get; set; }

        [ForeignKey("team2")]
        [Required]
        public int team2_ID { get; set; }
        public virtual Team Team2 { get; set; }

        [Required]
        public int team1_goals { get; set; }

        [Required]
        public int team2_goals { get; set; }

        [Required]
        public DateTime time { get; set; }

        public int winner_ID { get; set; }
    }
}
