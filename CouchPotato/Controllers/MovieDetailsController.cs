using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CouchPotato.Models;
using System.Collections;
using CouchPotato.ViewModels;

namespace CouchPotato.Controllers
{
    public class MovieDetailsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: MovieDetails/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<string> genres = new List<string>();
            foreach (var genre in db.MovieGenre.Join(db.Genres, mg => mg.GenreId, g => g.GenreId, (mg, g) => new { MovieGenre = mg, Genres = g })
                            .Where(m => m.MovieGenre.MovieId == id))
            {
                genres.Add(genre.Genres.Genre);
            }

            List<string> actors = new List<string>();
            foreach (var actor in db.MovieCast.Join(db.Actors, mc => mc.ActorId, a => a.ID, (mc, a) => new { MovieCast = mc, Actors = a })
                .Where(m => m.MovieCast.MovieId == id))
            {
                actors.Add(actor.Actors.Name);
            }

            var details = db.MovieDetails
                .Join(db.MoviesList, md => md.MovieId, ml => ml.MovieId, (md, ml) => new { MovieDetails = md, MoviesList = ml })
                .Where(m => m.MovieDetails.MovieId == id).FirstOrDefault();


            if (details == null)
            {
                return HttpNotFound();
            }

            MovieDetail movieDetail = new MovieDetail();
            movieDetail.MovieDetails = details.MovieDetails;
            movieDetail.MoviesList = details.MoviesList;
            movieDetail.Genres = String.Join(", ", genres);
            movieDetail.Actors = String.Join(", ", actors);

            HomeView homefilter = new HomeView();
            homefilter.MovieDetailView = movieDetail;
            homefilter.Genre = db.Genres.ToList();
            homefilter.Languages = db.Languages.ToList();

            return this.View(homefilter);
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
