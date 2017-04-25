using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CouchPotato.Models
{
    [Table("AdminUsers")]
    public class AdminUsers
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a valid Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}