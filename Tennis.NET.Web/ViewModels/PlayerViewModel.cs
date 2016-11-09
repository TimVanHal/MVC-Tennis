using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tennis.NET.Models;

namespace Tennis.NET.Web.ViewModels
{
    public class PlayerViewModel
    {
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Spelersnummer")]
        public int PlayerNr { get; set; }

        [MaxLength(50)]
        [Display(Name = "Achternaam")]
        public String PlayerLastName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Voornaam")]
        public String PlayerFirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Geboortedatum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PlayerBirthDay { get; set; }

        [Display(Name = "Geslacht")]
        public Gender? PlayerGender { get; set; }

        [Display(Name = "Jaar van toetreding")]
        public int? PlayerYearOfJoin { get; set; }

        [Display(Name = "Straatnaam")]
        [MaxLength(50)]
        public String PlayerStreet { get; set; }

        [Display(Name = "Huisnr")]
        [Range(1, int.MaxValue)]
        public int? PlayerHouseNr { get; set; }

        [Display(Name = "Bus")]
        public String PlayerMailBox { get; set; }

        [Display(Name = "Postcode")]
        public String PlayerZipcode { get; set; }

        [Display(Name = "Gemeente")]
        public String PlayerCity { get; set; }

        [Display(Name = "Telefoonnr")]
        public String PlayerPhoneNr { get; set; }

        [Display(Name = "Federatienr")]
        public int? PlayerFederationNr { get; set; }

        [Display(Name = "Naam")]
        public String FullName
        {
            get
            {
                return PlayerLastName + " " + PlayerFirstName;
            }
        }

        public IEnumerable<SelectListItem> AllRoles { get; set; }

        public List<int> SelectedRoles { get; set; }
    }
}