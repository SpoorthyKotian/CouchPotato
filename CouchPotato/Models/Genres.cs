using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("Genre")]
    public class Genres
    {
        [Key]
        public int GenreId { get; set; }
        public string Genre { get; set; }
    }
}