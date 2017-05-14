using CouchPotato.Models;
using CouchPotato.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    public class HomeView
    {
        public List<MovieDetails> Movies { get; set; }
        public MovieDetail MovieDetailView { get; set; }
        //public MoviesList MoviesList { get; set; }
        public List<Genres> Genre { get; set; }
        public List<Languages> Languages { get; set; }
        public List<Actors> Actors { get; set; }


    }
}