using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CouchPotato.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<HardDrives> HardDrives { get; set; }

        public DbSet<Types> Types { get; set; }

        public DbSet<Languages> Languages { get; set; }

        public DbSet<MoviesList> MoviesList { get; set; }

        public DbSet<MovieDetails> MovieDetails { get; set; }

        public DbSet<Genres> Genres { get; set; }

        public DbSet<Actors> Actors { get; set; }

        public DbSet<MovieGenre> MovieGenre { get; set; }

        public DbSet<MovieCast> MovieCast { get; set; }

        public DbSet<AdminUsers> AdminUsers { get; set; }

    }
}