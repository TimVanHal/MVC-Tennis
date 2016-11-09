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
    public class FineController : Controller
    {
        private FineRepository repo = new FineRepository();
        private PlayerRepository playerRepo = new PlayerRepository();

        // GET: /Fine/
        public ActionResult Index(int? playerId)
        {
            List<Fine> fines = repo.getFines();
            ViewBag.filter = false;
            if (playerId != null)
            {
                ViewBag.filter = true;
                List<Fine> filteredList = new List<Fine>();
                foreach(Fine f in fines)
                {
                    if (f.PlayerId == playerId)
                    {
                        filteredList.Add(f);
                    }
                }
                fines = filteredList;
            }
            return View(fines);
        }

        // GET: /Fine/Create
        public ActionResult Create()
        {
            ViewBag.PlId = new SelectList(playerRepo.getPlayers(), "Id", "FullName");
            return View();
        }

        // POST: /Fine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,PlayerId,FineDate,Amount")] Fine fine)
        {
            if (ModelState.IsValid)
            {
                repo.createFine(fine);
                return RedirectToAction("Index");
            }

            ViewBag.PlId = new SelectList(playerRepo.getPlayers(), "Id", "FullName", fine.PlayerId);
            return View(fine);
        }

        // GET: /Fine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = repo.getFineById(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlId = new SelectList(playerRepo.getPlayers(), "Id", "FullName", fine.PlayerId);
            return View(fine);
        }

        // POST: /Fine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,PlayerId,FineDate,Amount")] Fine fine)
        {
            if (ModelState.IsValid)
            {
                repo.modifyFine(fine);
                return RedirectToAction("Index");
            }
            ViewBag.PlId = new SelectList(playerRepo.getPlayers(), "Id", "FullName", fine.PlayerId);
            return View(fine);
        }

        // GET: /Fine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = repo.getFineById(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // POST: /Fine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.deleteFine(id);
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
