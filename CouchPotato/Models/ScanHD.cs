using CouchPotato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CouchPotato.ViewModels
{
    public class ScanHD

    {
        public List<HardDrives> hardDrives { get; set; }
        public List<Types> types { get; set; }
        public List<MoviesList> movieslist { get; set; }
    }
}