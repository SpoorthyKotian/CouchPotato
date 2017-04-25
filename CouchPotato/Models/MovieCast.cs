using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("MovieCast")]
    public class MovieCast
    {
        [Key]
        public int ID { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }

    }
}