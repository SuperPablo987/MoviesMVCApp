using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesData
{
    public class Movie
    {
        // EF will instruct the database to automatically generate value for this pk
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter a year.")]
        [Range(1889, 2050, ErrorMessage = "Year must be between 1889 and now.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please enter a rating.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        [Required(ErrorMessage = "Please enter a genre.")]
        [Display(Name = "Genre")]
        public string? GenreId { get; set; }

        // navigation property (don't get loaded unless we use "Include" method in MovieManager. We need the ? otherwise the validation will not work
        public Genre? Genre { get; set; }
    }
}
