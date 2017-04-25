using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CouchPotato.Models;
using PagedList;
using CouchPotato.Helper;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CouchPotato.Controllers
{
    [Authorize]
    public class MoviesListController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        private OmdbApiHelper service = new OmdbApiHelper();

        // GET: MoviesList
        public ActionResult Index(string sortOrder, string searchMovie, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchMovie != null)
            {
                page = 1;
            }
            else
            {
                searchMovie = currentFilter;
            }

            ViewBag.CurrentFilter = searchMovie;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var movie = from m in db.MoviesList
                        select m;
            if (!String.IsNullOrEmpty(searchMovie))
            {
                movie = movie.Where(m => m.MovieName.Contains(searchMovie));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    movie = movie.OrderByDescending(m => m.MovieName);
                    break;
                default:
                    movie = movie.OrderBy(m => m.MovieName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(movie.ToPagedList(pageNumber, pageSize));
        }

        // GET: MoviesList/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoviesList moviesList = db.MoviesList.Find(id);
            if (moviesList == null)
            {
                return HttpNotFound();
            }
            return View(moviesList);
        }

        // GET: MoviesList/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoviesList moviesList = db.MoviesList.Find(id);
            if (moviesList == null)
            {
                return HttpNotFound();
            }
            return View(moviesList);
        }

        // POST: MoviesList/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoviesList moviesList = db.MoviesList.Find(id);
            db.MoviesList.Remove(moviesList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       // GET: MoviesList/Sync
        public async Task<ActionResult> Sync(int id, MovieDetails MovieDetails)
        {
            MoviesList movieList = db.MoviesList.Find(id);

            OmdbApi omdbApi = await service.GetMovieDetailsFromApi(movieList.MovieName);

            if (omdbApi.IMDBId != null)
            {
                ProcessMovieData(omdbApi, id);
            }

            return RedirectToAction("Index");
        }

        private void ProcessMovieData(OmdbApi omdbApi, int movieid)
        {

            List<string> genreList = omdbApi.Genre.Split(',').Select(g => g.Trim()).ToList();
            List<int> genreIdList = ProcessGenre(genreList);

            List<string> actorList = omdbApi.Actors.Split(',').Select(a => a.Trim()).ToList();
            List<int> actorIdList = ProcessActors(actorList);

            var dbContextTransaction = db.Database.BeginTransaction();

            ProcessMovieGenre(genreIdList, movieid);

            ProcessMovieCast(actorIdList, movieid);

            ProcessMovieDetails(omdbApi, movieid);

            dbContextTransaction.Commit();
        }

        //Insert to Genre table and get genreId
        private List<int> ProcessGenre(List<string> genreList)
        {
            List<int> genreIdList = new List<int>();
            foreach (var gen in genreList)
            {
                Genres genreModel = db.Genres.Where(g => g.Genre.Contains(gen)).FirstOrDefault();
                if (genreModel == null)
                {
                    genreModel = new Genres();
                    genreModel.Genre = gen;
                    db.Genres.Add(genreModel);
                    db.SaveChanges();
                }
                genreIdList.Add(genreModel.GenreId);
            }

            return genreIdList;
        }

        //Insert MovieId and GenreId to MovieGenre table
        private void ProcessMovieGenre(List<int> genreIdList, int movieid)
        {
            foreach (int genid in genreIdList)
            {
                MovieGenre movieGenModel = db.MovieGenre.Where(g => g.GenreId.Equals(genid)).Where(g => g.MovieId.Equals(movieid)).FirstOrDefault();
                if (movieGenModel == null)
                {
                    movieGenModel = new MovieGenre();
                    movieGenModel.GenreId = genid;
                    movieGenModel.MovieId = movieid;
                    db.MovieGenre.Add(movieGenModel);
                    db.SaveChanges();
                }
            }
        }

        //Insert to Actors table and get Id
        private List<int> ProcessActors(List<string> actorList)
        {
            List<int> actorIdList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                string actor = actorList[i];
                Actors actorModel = db.Actors.Where(a => a.Name.Contains(actor)).FirstOrDefault();
                if (actorModel == null)
                {
                    actorModel = new Actors();
                    actorModel.Name = actor;
                    db.Actors.Add(actorModel);
                    db.SaveChanges();
                }
                actorIdList.Add(actorModel.ID);
            }
            return actorIdList;
        }

        //Insert MovieId and ActorId to MovieCast table
        private void ProcessMovieCast(List<int> actorIdList, int movieid)
        {
            foreach (int actorid in actorIdList)
            {
                MovieCast movieCastModel = db.MovieCast.Where(a => a.ActorId.Equals(actorid)).Where(a => a.MovieId.Equals(movieid)).FirstOrDefault();
                if (movieCastModel == null)
                {
                    movieCastModel = new MovieCast();
                    movieCastModel.ActorId = actorid;
                    movieCastModel.MovieId = movieid;
                    db.MovieCast.Add(movieCastModel);
                    db.SaveChanges();
                }
            }
        }

        //Insert data into MovieDetails table
        private void ProcessMovieDetails(OmdbApi omdbApi, int movieid)
        {
            List<string> langList = omdbApi.Language.Split(',').Select(l => l.Trim()).ToList();
            MovieDetails movieDetails = db.MovieDetails.Where(m => m.MovieId.Equals(movieid)).FirstOrDefault();
            bool exits = true;

            if (movieDetails == null)
            {
                exits = false;
                movieDetails = new MovieDetails();
            }

            movieDetails.MovieId = movieid;
            movieDetails.Year = omdbApi.Year;
            movieDetails.Runtime = omdbApi.Runtime;
            movieDetails.Plot = omdbApi.Plot;
            movieDetails.Poster = omdbApi.Poster;
            movieDetails.Language = langList.First();
            movieDetails.IMDBRating = omdbApi.IMDBRating;
            movieDetails.IMDBId = omdbApi.IMDBId;

            if (exits)
            {
                db.Entry(movieDetails).State = EntityState.Modified;
            }
            else
            {
                db.MovieDetails.Add(movieDetails);
            }

            db.SaveChanges();
        }

        public async Task<ActionResult> SyncAll()
        {
            List<MoviesList> movieList = db.MoviesList.Where(movList => !db.MovieDetails.Any(movDetails => movDetails.MovieId == movList.MovieId)).ToList();

            foreach (MoviesList eachMovie in movieList)
            {
                OmdbApi omdbApi = await service.GetMovieDetailsFromApi(eachMovie.MovieName);

                if (omdbApi.IMDBId != null)
                {
                    ProcessMovieData(omdbApi, eachMovie.MovieId);
                }
            }

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
