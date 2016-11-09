using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tennis.NET.Models;

namespace Tennis.NET.Web.ViewModels
{
    public class RoleViewModel
    {
        //public Role Role { get; set; }
        public int RoleId { get; set; }

        [Display(Name = "Naam")]
        public String RoleName { get; set; }

        public IEnumerable<SelectListItem> AllPlayers { get; set; }

        //private List<int> _selectedPlayers;
        //public List<int> SelectedPlayers 
        //{ 
        //    get 
        //    {
        //        if (_selectedPlayers == null)
        //        {
        //            _selectedPlayers = Role.Players.Select(p => p.Id).ToList();
        //        }
        //        return _selectedPlayers;
        //    }
        //    set
        //    {
        //        _selectedPlayers = value;
        //    }
        //}
        public List<int> SelectedPlayers { get; set; }
    }
}