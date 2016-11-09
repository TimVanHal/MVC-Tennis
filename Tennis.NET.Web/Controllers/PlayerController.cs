using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tennis.NET.Models;
using Tennis.NET.DataModel;
using Tennis.NET.Repositories;
using Tennis.NET.Web.ViewModels;

namespace Tennis.NET.Web.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerRepository repo = new PlayerRepository();
        private RoleRepository roleRepo = new RoleRepository();

        // GET: /Player/
        public ActionResult Index()
        {
            return View(repo.getPlayers());
        }

        // GET: /Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = repo.getPlayerById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: /Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,PlayerNr,LastName,FirstName,BirthDay,Gender,YearOfJoin,Street,HouseNr,MailBox,Zipcode,City,PhoneNr,FederationNr")] Player player)
        {
            if (ModelState.IsValid)
            {
                repo.createPlayer(player);
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: /Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = repo.getPlayerById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            var allRolesList = roleRepo.getRoles();
            var allRolesSelect = allRolesList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });
            var selectedRoles = player.Roles.Select(p => p.Id).ToList();
            PlayerViewModel pvm = new PlayerViewModel { PlayerId = player.Id, PlayerBirthDay = player.BirthDay, PlayerCity = player.City,
                PlayerFederationNr = player.FederationNr, PlayerFirstName = player.FirstName, PlayerGender = player.Gender, 
                PlayerHouseNr = player.HouseNr, PlayerLastName = player.LastName, PlayerMailBox = player.MailBox, PlayerNr = player.PlayerNr,
                PlayerPhoneNr = player.PhoneNr, PlayerStreet = player.Street, PlayerYearOfJoin = player.YearOfJoin, PlayerZipcode = player.Zipcode,
                AllRoles = allRolesSelect, SelectedRoles = selectedRoles };
            return View(pvm);
        }

        // POST: /Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                Player p = new Player
                {
                    BirthDay = pvm.PlayerBirthDay,
                    City = pvm.PlayerCity,
                    FederationNr = pvm.PlayerFederationNr,
                    FirstName = pvm.PlayerFirstName,
                    Gender = pvm.PlayerGender,
                    HouseNr = pvm.PlayerHouseNr,
                    Id = pvm.PlayerId,
                    LastName = pvm.PlayerLastName,
                    MailBox = pvm.PlayerMailBox,
                    PhoneNr = pvm.PlayerPhoneNr,
                    PlayerNr = pvm.PlayerNr,
                    Street = pvm.PlayerStreet,
                    YearOfJoin = pvm.PlayerYearOfJoin,
                    Zipcode = pvm.PlayerZipcode
                };
                p.Roles = new List<Role>();
                if (pvm.SelectedRoles != null && pvm.SelectedRoles.Count > 0)
                {
                    foreach (var r in pvm.SelectedRoles)
                    {
                        p.Roles.Add(roleRepo.getRoleById(r));
                    } 
                }
                repo.modifyPlayer(p);
                return RedirectToAction("Index");
            }
            var allRolesList = roleRepo.getRoles();
            var allRolesSelect = allRolesList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });
            pvm.AllRoles = allRolesSelect;
            return View(pvm);
        }

        // GET: /Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = repo.getPlayerById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: /Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.deletePlayer(id);
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
