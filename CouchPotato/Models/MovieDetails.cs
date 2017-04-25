using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("MovieDetails")]
    public class MovieDetails
    {
        [Key]
        public int ID { get; set; }
        public int MovieId { get; set; }
        public int Year { get; set; }
        public string Runtime { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Language { get; set; }
        public float IMDBRating { get; set; }
        public string IMDBId { get; set; }
    }
}