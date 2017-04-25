using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("Types")]
    public class Types
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}