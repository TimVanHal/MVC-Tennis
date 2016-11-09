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

namespace Tennis.NET.Web.Controllers
{
    public class TeamPlayerController : Controller
    {
        private TeamPlayerRepository repo = new TeamPlayerRepository();
        private TeamRepository teamRepo = new TeamRepository();
        private PlayerRepository playerRepo = new PlayerRepository();
        private DivisionRepository divisionRepo = new DivisionRepository();

        // GET: /TeamPlayer/
        public ActionResult Index(int? divId, int? playerId, int? teamId)
        {
            List<TeamPlayer> teamPlayers = repo.getTeamPlayers();
            ViewBag.filter = false;
            if (divId != null)
            {
                ViewBag.filter = true;
                List<TeamPlayer> filteredList = new List<TeamPlayer>();
                foreach (TeamPlayer tp in teamPlayers)
                {
                    if (tp.DivisionId == divId)
                    {
                        filteredList.Add(tp);
                    }
                }
                teamPlayers = filteredList;
            }
            if (playerId != null)
            {
                ViewBag.filter = true;
                List<TeamPlayer> filteredList = new List<TeamPlayer>();
                foreach (TeamPlayer tp in teamPlayers)
                {
                    if (tp.PlayerId == playerId)
                    {
                        filteredList.Add(tp);
                    }
                }
                teamPlayers = filteredList;
            }
            if (teamId != null)
            {
                ViewBag.filter = true;
                List<TeamPlayer> filteredList = new List<TeamPlayer>();
                foreach (TeamPlayer tp in teamPlayers)
                {
                    if (tp.TeamId == teamId)
                    {
                        filteredList.Add(tp);
                    }
                }
                teamPlayers = filteredList;
            }
            return View(teamPlayers);
        }

        // GET: /TeamPlayer/Create
        public ActionResult Create()
        {
            ViewBag.DivId = new SelectList(divisionRepo.getDivisions(), "Id", "Name");
            ViewBag.PlId = new SelectList(playerRepo.getPlayers(), "Id", "FullName");
            ViewBag.TmId = new SelectList(teamRepo.getTeams(), "Id", "Name");
            return View();
        }

        // POST: /TeamPlayer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TeamId,PlayerId,DivisionId")] TeamPlayer teamplayer)
        {
            if (ModelState.IsValid)
            {
                repo.createTeamPlayer(teamplayer);
                return RedirectToAction("Index");
            }

            ViewBag.DivId = new SelectList(divisionRepo.getDivisions(), "Id", "Name", teamplayer.DivisionId);
            ViewBag.PlId = new SelectList(playerRepo.getPlayers(), "Id", "FullName", teamplayer.PlayerId);
            ViewBag.TmId = new SelectList(teamRepo.getTeams(), "Id", "Name", teamplayer.TeamId);
            return View(teamplayer);
        }

        // GET: /TeamPlayer/Delete/5
        public ActionResult Delete(int? playerId, int? divisionId, int? teamId)
        {
            if (playerId == null || divisionId == null || teamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPlayer teamplayer = repo.getTeamPlayerById(playerId, divisionId, teamId);
            if (teamplayer == null)
            {
                return HttpNotFound();
            }
            return View(teamplayer);
        }

        // POST: /TeamPlayer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int playerId, int divisionId, int teamId)
        {
            repo.deleteTeamPlayer(playerId, divisionId, teamId);
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
