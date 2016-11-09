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
    public class RoleController : Controller
    {
        private RoleRepository repo = new RoleRepository();
        private PlayerRepository playerRepo = new PlayerRepository();

        // GET: /Role/
        public ActionResult Index(int? playerId)
        {
            List<Role> roles = new List<Role>();
            if (playerId != null)
            {
                ViewBag.filter = true;
                roles = repo.getRolesFiltered((int)playerId);
            }
            else
            {
                ViewBag.filter = false;
                roles = repo.getRoles();
            }
            return View(roles);
        }

        // GET: /Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = repo.getRoleById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                repo.createRole(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: /Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role r = repo.getRoleById(id);
            if (r == null)
            {
                return HttpNotFound();
            }
            var allPlayersList = playerRepo.getPlayers();
            var allPlayersSelect = allPlayersList.Select(o => new SelectListItem
            {
                Text = o.FullName,
                Value = o.Id.ToString()
            });
            var selectedPlayers = r.Players.Select(p => p.Id).ToList();
            RoleViewModel rvm = new RoleViewModel { RoleId = r.Id, RoleName = r.Name, AllPlayers = allPlayersSelect, SelectedPlayers = selectedPlayers };
            return View(rvm);
        }

        // POST: /Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                Role r = new Role { Id = rvm.RoleId, Name = rvm.RoleName };
                r.Players = new List<Player>();
                if (rvm.SelectedPlayers != null && rvm.SelectedPlayers.Count > 0)
                {
                    foreach (var p in rvm.SelectedPlayers)
                    {
                        r.Players.Add(playerRepo.getPlayerById(p));
                    } 
                }
                repo.modifyRole(r);
                return RedirectToAction("Index");
            }
            var allPlayersList = playerRepo.getPlayers();
            var allPlayersSelect = allPlayersList.Select(o => new SelectListItem
            {
                Text = o.FullName,
                Value = o.Id.ToString()
            });
            rvm.AllPlayers = allPlayersSelect;
            return View(rvm);
        }

        // GET: /Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = repo.getRoleById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.deleteRole(id);
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
