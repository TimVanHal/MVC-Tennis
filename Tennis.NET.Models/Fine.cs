using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tennis.NET.Models
{
    public class Fine
    {
        public int Id { get; set; }

        public Player Player { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum boete")]
        public DateTime FineDate { get; set; }

        [Required]
        [RegularExpression("([0-9]){1,7}[.]([0-9]){2}", ErrorMessage="Het bedrag moet van de vorm zijn x.##")]
        [Display(Name = "Bedrag (€)")]
        [MaxLength(10)]
        public String Amount { get; set; }
        //Bedrag als string omdat decimal en float beiden problemen gaven. Bij het gebruiken van @Html.EditorFor(), werden geen
        //decimale getallen herkend. 40 kon ingegeven worden, maar noch 40.99, noch 40,99 werden herkend als getal.
        //Zelfde probleem als hier: http://stackoverflow.com/questions/32176310/how-to-enter-decimal-value-in-asp-net-mvc
    }
}