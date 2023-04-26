using Microsoft.AspNetCore.Mvc;
using MoviesData;

namespace MoviesMVCApp.Components
{
    public class MoviesByGenreViewComponent:ViewComponent
    {
        private MovieContext dB; // context object
        // constructor with dependency injection
        public MoviesByGenreViewComponent(MovieContext context)
        {
            dB = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) // genre id
        {
            List<Movie> movies = null;
            if(id == "All")
            {
                movies = MovieManager.GetMovies(dB);
            }
            else // specific genre
            {
                movies = MovieManager.GetMoviesByGenre(dB, id);
            }
            return View(movies); // in Views/Shared/Components/MoviesByGenre/Default.cshtml
        }
    }
}
