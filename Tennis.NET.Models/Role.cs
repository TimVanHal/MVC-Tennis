using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tennis.NET.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Display(Name = "Naam")]
        public String Name { get; set; }

        [Display(Name = "Leden")]
        public virtual List<Player> Players { get; set; }
    }
}