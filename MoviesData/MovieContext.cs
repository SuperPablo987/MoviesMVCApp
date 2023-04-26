using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesData
{
    public class MovieContext:DbContext
    {
        // we also comment out the default constructor because we will no longer need it with our one below (but we could leave it if we want and everything would be fine)
        public MovieContext() : base() { } // just calls the base class constructor

        // need to add another constructor for connection string in appsettings.json
        // this is an option of dependency injection where we have the object of class A used to create class B (which gets rid of tight coupling)
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // we comment out this method because it is used to build the database.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // provide connection string
        //    optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=Movies;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 4,
                    Name = "Casablanca",
                    Year = 1943,
                    Rating = 5,
                    GenreId = "D"
                },
                new Movie
                {
                    MovieId = 2,
                    Name = "Wonder Woman",
                    Year = 2017,
                    Rating = 3,
                    GenreId = "A"
                },
                new Movie
                {
                    MovieId = 3,
                    Name = "Moonstruck",
                    Year = 1988,
                    Rating = 4,
                    GenreId = "R"
                }
            );
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = "A", Name = "Action" },
                new Genre { GenreId = "C", Name = "Comedy" },
                new Genre { GenreId = "D", Name = "Drama" },
                new Genre { GenreId = "H", Name = "Horror" },
                new Genre { GenreId = "M", Name = "Musical" },
                new Genre { GenreId = "R", Name = "RomCom" },
                new Genre { GenreId = "S", Name = "SciFi" }
            );
        }
    }
}
