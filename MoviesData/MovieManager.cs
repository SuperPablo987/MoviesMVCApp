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
        /// retrieves list of movies from the database
        /// </summary>
        /// <returns>list of movies or null if none</returns>
        public static List<Movie> GetMovies()
        {
            List<Movie> movies = null;
            using (MovieContext dB = new MovieContext()) 
            {
                movies = dB.Movies.ToList();
            }

            return movies;
        }
    }
}
