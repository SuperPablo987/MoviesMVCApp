using Microsoft.EntityFrameworkCore; // for include
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesData
{
    /// <summary>
    /// methods for working with Movie table in Movie database
    /// </summary>
    public static class MovieManager
    {
        /// <summary>
        /// retrieve all movies
        /// </summary>
        /// <param name="dB">context object</param>
        /// <returns>list of movies or null if none</returns>
        public static List<Movie> GetMovies(MovieContext dB) // add MovieContext object here instead (dependency injection again)
        {
            List<Movie> movies = null;
            // we need to comment this out because we are passing the object class as part of the method call now
            //using (MovieContext dB = new MovieContext()) // when we add the connection string to appsetting.json we now have to pass the options variable
            //{
            // add include method to include names of genre on output
            movies = dB.Movies.Include(m => m.Genre).ToList();
            //}

            return movies;
        }
        /// <summary>
        /// retrieve movie genres
        /// </summary>
        /// <param name="dB">context object</param>
        /// <returns>list of genres</returns>
        public static List<Genre> GetGenres(MovieContext dB)
        {
            List<Genre> genres = dB.Genres.OrderBy(g => g.Name).ToList();
            return genres;
        }
        /// <summary>
        /// get movie with given ID
        /// </summary>
        /// <param name="dB">context object</param>
        /// <param name="id">ID of the movie to find</param>
        /// <returns>movie or null if not found</returns>
        public static Movie? GetMovieByID(MovieContext dB, int id)
        {
            Movie? movie = dB.Movies.Find(id);
            return movie;
        }
        /// <summary>
        /// add another movie to the table
        /// </summary>
        /// <param name="dB">context object</param>
        /// <param name="movie">new movie to add</param>
        public static void AddMovie(MovieContext dB, Movie movie)
        {
            if(movie != null)
            {
                dB.Movies.Add(movie);
                dB.SaveChanges();
            }
        }
        /// <summary>
        /// update movie with given id using new movie data
        /// </summary>
        /// <param name="dB">context object</param>
        /// <param name="id">ID of given movie to update</param>
        /// <param name="newMovie">new movie data</param>
        public static void UpdateMovie(MovieContext dB, int id, Movie newMovie)
        {
            Movie movie = dB.Movies.Find(id);
            if (movie !=null)
            {

                // copy over new movie data
                movie.Name = newMovie.Name;
                movie.Year = newMovie.Year;
                movie.Rating = newMovie.Rating;
                movie.GenreId = newMovie.GenreId;
                dB.SaveChanges(); 
            }
        }
        /// <summary>
        /// delete movie with given id
        /// </summary>
        /// <param name="dB">context object</param>
        /// <param name="id">ID of the movie to delete</param>
        public static void DeleteMovie(MovieContext dB, int id)
        {
            Movie? movie = dB.Movies.Find(id);
            if(movie != null)
            {
                dB.Movies.Remove(movie);
                dB.SaveChanges();
            }
        }
    }
}
