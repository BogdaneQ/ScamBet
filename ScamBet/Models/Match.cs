using Microsoft.VisualBasic;
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

        [ForeignKey("Team")]
        [Required]
        public string team1_ID { get; set; }
        public virtual Team team1 { get; set; }

        [ForeignKey("Team")]
        [Required]
        public string team2_ID { get; set; }
        public virtual Team team2 { get; set; }


        public Team? winner { get; set; }

        [Required]
        public int team1_goals { get; set; }

        [Required]
        public int team2_goals { get; set; }

        [Required]
        public int team1_fouls { get; set; }

        [Required]
        public int team2_fouls { get; set; }

        [Required]
        public int team1_red_cards { get; set; }

        [Required]
        public int team2_red_cards { get; set; }

        [Required]
        public int team1_yellow_cards { get; set; }

        [Required]
        public int team2_yellow_cards { get; set; }

        [Required]
        public int team1_shots { get; set; }

        [Required]
        public int team2_shots { get; set; }

        [Required]
        public int team1_shots_ontarget { get; set; }

        [Required]
        public int team2_shots_ontarget { get; set; }

        [Required]
        public int team1_corners { get; set; }

        [Required]
        public int team2_corners { get; set; }

        [Required]
        public DateTime time { get; set; }
    }
}
