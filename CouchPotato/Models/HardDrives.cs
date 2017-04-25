using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("HardDrives")]
    public class HardDrives
    {
        [Key]
        public int HDId { get; set; }
        public string Name { get; set; }
    }
}