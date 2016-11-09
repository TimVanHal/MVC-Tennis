using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tennis.NET.Models
{
    public class Game
    {
        public int Id { get; set; }

        public Player Player1 { get; set; }

        [Required]
        public int Player1Id { get; set; }

        public Player Player2 { get; set; }

        [Required]
        public int Player2Id { get; set; }

        [Required]
        [Display(Name = "Score speler 1")]
        [Range(0, 10)]
        public int Player1Score { get; set; }

        [Required]
        [Display(Name = "Score speler 2")]
        [Range(0, 10)]
        public int Player2Score { get; set; }

        [NotMapped]
        [Display(Name = "Score")]
        public String Score { 
            get {
                return Player1Score + " - " + Player2Score;
            } 
        }
    }
}