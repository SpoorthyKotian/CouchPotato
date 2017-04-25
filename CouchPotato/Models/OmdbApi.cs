using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    public class OmdbApi
    {
        public int Year { get; set; }
        public string Runtime { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Language { get; set; }
        public float IMDBRating { get; set; }
        public string IMDBId { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
    }
}