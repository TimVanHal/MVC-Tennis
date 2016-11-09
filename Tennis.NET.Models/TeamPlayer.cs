using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tennis.NET.Models
{
    public class TeamPlayer
    {
        public Player Player { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }

        public Team Team { get; set; }

        [Required]
        [Key, Column(Order = 0)]
        public int TeamId { get; set; }

        public Division Division { get; set; }

        [Required]
        [Key, Column(Order = 2)]
        public int DivisionId { get; set; }
    }
}