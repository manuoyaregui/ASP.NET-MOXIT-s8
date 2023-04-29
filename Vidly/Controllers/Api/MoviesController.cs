using System.Data.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vidly.Dtos;
using Vidly.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            var moviesDtos = 
                _context.Movies
                    .Include(m => m.Genre)
                    .ToList()
                    .Select(Mapper.Map<Movie, MovieDto>);
            return moviesDtos;
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id:int}")]
        public ActionResult<MovieDto> GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/<MoviesController>
        [HttpPost]
        public ActionResult<MovieDto> CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            
            
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.GetDisplayUrl() + "/" + movie.Id), movieDto);
        }

        // PUT api/<MoviesController>/5
        [HttpPut]
        public ActionResult<MovieDto> UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault( m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok(movieInDb);

        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id:int}")]
        public ActionResult<MovieDto> DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieInDb);

            _context.SaveChanges();

            return Ok(movieInDb);
        }
    }
}
