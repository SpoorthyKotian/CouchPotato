using CouchPotato.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CouchPotato.Helper;
using System.Data.Entity;

namespace CouchPotato.Controllers
{
    public class AdminController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Admin/ScanHD
        public ActionResult ScanHD()
        {
            var viewModel = new ViewModels.ScanHD();

            viewModel.hardDrives = db.HardDrives.ToList();

            viewModel.types = db.Types.ToList();

            return View(viewModel);
        }


        // POST: Admin/ScanHD
        [HttpPost]
        public ActionResult ScanHD(FormCollection form)
        {
            string path = form["drivePath"].ToString();
            int Hdid = Convert.ToInt32(form["Harddrives"].ToString());
            int Typeid = Convert.ToInt32(form["Type"].ToString());

            List<KeyValuePair<string, string>> kvplist = DirectoryHelper.GetMoviePath(path);
            AddToMovieList(Hdid, Typeid, kvplist);
           
            return RedirectToAction("Index", "MoviesList");
        }

        // Inserts data to MovieList table
        private void AddToMovieList(int Hdid, int Typeid, List<KeyValuePair<string, string>> Kvplist)
        {
            int countall = Kvplist.Count();
            int countinsert = 0;
            int countupdate = 0;

            foreach (KeyValuePair<string, string> kvp in Kvplist)
            {
                string MovieName = kvp.Key;
                
                MoviesList moviesList = db.MoviesList.Where(m => m.MovieName.Equals(MovieName)).FirstOrDefault();
                bool exits = true;

                if (moviesList == null)
                {
                    exits = false;
                    moviesList = new MoviesList();
                }

                moviesList.HDId = Hdid;
                moviesList.TypeId = Typeid;
                moviesList.MovieName = MovieName;
                moviesList.Path = kvp.Value;

                if (exits)
                {
                    countupdate += 1;
                    db.Entry(moviesList).State = EntityState.Modified;
                }
                else
                {
                    countinsert += 1;
                    db.MoviesList.Add(moviesList);
                }

                db.SaveChanges();
            }
            int countremain = countall - (countinsert + countupdate);
        }

    }

}