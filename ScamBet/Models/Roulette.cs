﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScamBet.Entities
{
    public class Roulette
    {
        [Key]
        [Required]
        public int roulette_ID { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [Range(1,10)]
        public int difficulty { get; set; }

    }
}
