using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tennis.NET.Models
{
    public class Division
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Divisienaam")]
        public String Name { get; set; }

        [Display(Name = "Teams en spelers")]
        public virtual List<TeamPlayer> TeamPlayers { get; set; }
    }
}