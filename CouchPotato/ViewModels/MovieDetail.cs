using CouchPotato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouchPotato.ViewModels
{
    public class MovieDetail
    {
        public MovieDetails MovieDetails { get; set; }

        public MoviesList MoviesList { get; set; }

        //public MovieGenre MovieGenre { get; set; }

        //public MovieCast MovieCast { get; set; }

        public string Genres { get; set; }

        public string Actors { get; set; }
    }
}