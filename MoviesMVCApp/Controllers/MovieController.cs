﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesData;

namespace MoviesMVCApp.Controllers
{
    public class MovieController : Controller
    {
        // we need to create a MovieContext object to help with dependency injection
        private MovieContext _context { get; set; } //auto-implemented property

        // context get injected to the constructor (dependency injection)
        public MovieController(MovieContext context)
        {
            _context = context;
        }


        // GET: MovieController
        // list of movies
        public ActionResult Index()
        {
            List<Movie> movies = MovieManager.GetMovies(_context); // we pass _context here to help with dependency injection
            return View(movies);
        }

        // filter movies by genre 
        public ActionResult FilteredList()
        {
            // technically we should refactor this code block because its the same for post
            // prepare list of genres for the drop down list
            List<Genre> genres = MovieManager.GetGenres(_context); // we pass _context here to help with dependency injection
            var list = new SelectList(genres, "GenreId", "Name").ToList();
            list.Insert(0, new SelectListItem("All", "All")); // add all as first option
            ViewBag.Genres = list;

            List<Movie> movies = MovieManager.GetMovies(_context); // all movies
            return View(movies);
        }
        [HttpPost]
        public ActionResult FilteredList(string id = "All")
        {
            // retain genres for drop down list and selected item
            List<Genre> genres = MovieManager.GetGenres(_context); // we pass _context here to help with dependency injection
            var list = new SelectList(genres, "GenreId", "Name").ToList();
            list.Insert(0, new SelectListItem("All", "All")); // add all as first option
            
            foreach(var item in list) // find selected item
            {
                if (item.Value == id)
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.Genres = list;
            List<Movie> movies;
            if(id == "All")
            {
                movies = MovieManager.GetMovies(_context); // all movies
            }
            else // genre selected
            {
                movies = MovieManager.GetMoviesByGenre(_context, id);
            }
            return View(movies);

        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            Movie? movie = MovieManager.GetMovieByID(_context, id);
            return View(movie);
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            // prepare list of genres for the drop down list
            List<Genre> genres = MovieManager.GetGenres(_context); // we pass _context here to help with dependency injection
            var list = new SelectList(genres, "GenreId", "Name");
            ViewBag.Genres = list; // used to pass the _context to the empty movie for the list of genres
            Movie movie = new Movie(); // empty movie object
            return View(movie);
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie newMovie) // data collected from the form
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MovieManager.AddMovie(_context, newMovie);
                    return RedirectToAction(nameof(Index));
                }
                else // validation errors
                {
                    return View(newMovie);
                }
                
            }
            catch
            {
                return View(newMovie);
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            // prepare list of genres for the drop down list
            List<Genre> genres = MovieManager.GetGenres(_context); // we pass _context here to help with dependency injection
            var list = new SelectList(genres, "GenreId", "Name").ToList();
            ViewBag.Genres = list; // used to pass the _context to the empty movie for the list of genres
            Movie? movie = MovieManager.GetMovieByID(_context, id);
            return View(movie);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Movie newMovie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MovieManager.UpdateMovie(_context, id, newMovie);
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    return View(newMovie);
                }
            }
            catch
            {
                return View(newMovie);
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            Movie? movie = MovieManager.GetMovieByID(_context, id);
            return View(movie);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Movie movie)
        {
            try
            {
                MovieManager.DeleteMovie(_context, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
