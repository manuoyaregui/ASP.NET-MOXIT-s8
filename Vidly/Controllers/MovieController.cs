using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }

        public IActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var genres = _context.Genres.ToList();
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = genres
            };


            return View("MovieForm", viewModel);
        }

        [Route("/movies/released/{year}/{month:regex(\\d{{2}}):range(1,12)}")]
        public IActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public IActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres
            };


            return View("MovieForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {

            if (! ModelState.IsValid)
            {
                var genres = _context.Genres.ToList();
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = genres
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.DateAdded = movie.DateAdded;
                movieInDB.Stock = movie.Stock;
                movieInDB.GenreID = movie.GenreID;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }
    }
}
