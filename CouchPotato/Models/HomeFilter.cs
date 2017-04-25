using CouchPotato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    public class HomeFilter
    {
        public List<MovieDetails> MovieDetails { get; set; }
        //public MoviesList MoviesList { get; set; }
        public List<Genres> Genre { get; set; }
        public List<Languages> Languages { get; set; }
       // public Actors Actors { get; set; }


    }
}