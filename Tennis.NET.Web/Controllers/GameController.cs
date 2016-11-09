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
    public class GameController : Controller
    {
        private GameRepository repo = new GameRepository();
        private PlayerRepository playerRepo = new PlayerRepository();

        // GET: /Game/
        public ActionResult Index()
        {
            return View(repo.getGames());
        }

        // GET: /Game/Create
        public ActionResult Create()
        {
            ViewBag.P1Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName");
            ViewBag.P2Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName");
            return View();
        }

        // POST: /Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Player1Id,Player2Id,Player1Score,Player2Score")] Game game)
        {
            if (ModelState.IsValid)
            {
                repo.createGame(game);
                return RedirectToAction("Index");
            }

            ViewBag.P1Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName", game.Player1Id);
            ViewBag.P2Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName", game.Player2Id);
            return View(game);
        }

        // GET: /Game/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = repo.getGameById(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.P1Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName", game.Player1Id);
            ViewBag.P2Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName", game.Player2Id);
            return View(game);
        }

        // POST: /Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Player1Id,Player2Id,Player1Score,Player2Score")] Game game)
        {
            if (ModelState.IsValid)
            {
                repo.modifyGame(game);
                return RedirectToAction("Index");
            }
            ViewBag.P1Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName", game.Player1Id);
            ViewBag.P2Id = new SelectList(playerRepo.getPlayers(), "Id", "FullName", game.Player2Id);
            return View(game);
        }

        // GET: /Game/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = repo.getGameById(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: /Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.deleteGame(id);
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
