using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("Languages")]
    public class Languages
    {
        [Key]
        public int ID { get; set; }
        public string Language { get; set; }
    }
}