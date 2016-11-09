using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tennis.NET.Annotations;

namespace Tennis.NET.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Spelersnummer")]
        public int PlayerNr { get; set; }

        [MaxLength(50)]
        [Display(Name = "Achternaam")]
        public String LastName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Voornaam")]
        public String FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Geboortedatum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Geslacht")]
        public Gender? Gender { get; set; }

        [Display(Name = "Jaar van toetreding")]
        public int? YearOfJoin { get; set; }

        [Display(Name = "Straatnaam")]
        [MaxLength(50)]
        public String Street { get; set; }

        [Display(Name = "Huisnr")]
        [Range(1, 1000)]
        public int? HouseNr { get; set; }

        [Display(Name = "Bus")]
        public String MailBox { get; set; }

        [Display(Name = "Postcode")]
        public String Zipcode { get; set; }

        [Display(Name = "Gemeente")]
        public String City { get; set; }

        [Display(Name = "Telefoonnr")]
        public String PhoneNr { get; set; }

        [Display(Name = "Federatienr")]
        public int? FederationNr { get; set; }

        [Display(Name = "Rollen")]
        public virtual List<Role> Roles { get; set; }

        [Display(Name = "Teams en divisies")]
        public virtual List<TeamPlayer> TeamPlayers { get; set; }

        [Display(Name = "Boetes")]
        public virtual List<Fine> Fines { get; set; }

        [NotMapped]
        [Display(Name = "Naam")]
        public String FullName {
            get
            {
                return LastName + " " + FirstName;
            }
        }
    }
}