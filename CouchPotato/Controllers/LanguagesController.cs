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
    public class LanguagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Languages
        public ActionResult Index()
        {
            return View(db.Languages.ToList());
        }

        // GET: Languages/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Languages languages = db.Languages.Find(id);
            if (languages == null)
            {
                return HttpNotFound();
            }
            return View(languages);
        }

        // GET: Languages/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Languages/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ID,Language")] Languages languages)
        {
            if (ModelState.IsValid)
            {
                db.Languages.Add(languages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(languages);
        }

        // GET: Languages/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Languages languages = db.Languages.Find(id);
            if (languages == null)
            {
                return HttpNotFound();
            }
            return View(languages);
        }

        // POST: Languages/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Language")] Languages languages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(languages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(languages);
        }

        // GET: Languages/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Languages languages = db.Languages.Find(id);
            if (languages == null)
            {
                return HttpNotFound();
            }
            return View(languages);
        }

        // POST: Languages/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Languages languages = db.Languages.Find(id);
            db.Languages.Remove(languages);
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
