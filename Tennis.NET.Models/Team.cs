using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tennis.NET.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Teamnaam")]
        public String Name { get; set; }

        [Display(Name = "Spelers en divisies")]
        public virtual List<TeamPlayer> TeamPlayers { get; set; }
    }
}