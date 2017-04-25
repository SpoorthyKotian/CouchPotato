using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CouchPotato.Models;

namespace CouchPotato.Controllers
{
    [Authorize]
    public class HardDrivesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: HardDrives
        public ActionResult Index()
        {
            return View(db.HardDrives.ToList());
        }

        // GET: HardDrives/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardDrives hardDrives = db.HardDrives.Find(id);
            if (hardDrives == null)
            {
                return HttpNotFound();
            }
            return View(hardDrives);
        }

        // GET: HardDrives/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: HardDrives/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "HDId,Name")] HardDrives hardDrives)
        {
            if (ModelState.IsValid)
            {
                db.HardDrives.Add(hardDrives);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hardDrives);
        }

        // GET: HardDrives/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardDrives hardDrives = db.HardDrives.Find(id);
            if (hardDrives == null)
            {
                return HttpNotFound();
            }
            return View(hardDrives);
        }

        // POST: HardDrives/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HDId,Name")] HardDrives hardDrives)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hardDrives).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hardDrives);
        }

        // GET: HardDrives/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardDrives hardDrives = db.HardDrives.Find(id);
            if (hardDrives == null)
            {
                return HttpNotFound();
            }
            return View(hardDrives);
        }

        // POST: HardDrives/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HardDrives hardDrives = db.HardDrives.Find(id);
            db.HardDrives.Remove(hardDrives);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
