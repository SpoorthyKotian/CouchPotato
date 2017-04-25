using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("MoviesList")]
    public class MoviesList
    {
        [Key]
        public int MovieId { get; set; }
        public int HDId { get; set; }
        public int TypeId { get; set; }
        public string MovieName { get; set; }
        public string Path { get; set; }

    }
}