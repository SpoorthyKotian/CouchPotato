using CouchPotato.Models;
using CouchPotato.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CouchPotato.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            MovieDetails md = new MovieDetails();
            HomeFilter homefilter = new HomeFilter();
            homefilter.MovieDetails = db.MovieDetails.OrderByDescending(p => p.Year).Take(4).ToList();
            homefilter.Genre = db.Genres.ToList();
            homefilter.Languages = db.Languages.ToList();
            return this.View(homefilter);
        }

        
    }
}