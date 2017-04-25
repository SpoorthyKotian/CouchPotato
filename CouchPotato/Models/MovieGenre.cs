using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("MovieGenre")]
    public class MovieGenre
    {
        [Key]
        public int ID { get; set; }
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}