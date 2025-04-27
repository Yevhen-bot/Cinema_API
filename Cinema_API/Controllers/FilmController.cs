using AutoMapper;
using Cinema_API.DTOs;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public FilmController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetFilms()
        {
            return Ok(_context.Films.ToList().Select(f => _mapper.Map<GetFilmModel>(f)));
        }

        [HttpGet("{id}")]
        public IActionResult GetFilm(int id)
        {
            var film = _context.Films.FirstOrDefault(f => f.Id == id);
            if(film == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetFilmModel>(film));
        }

        [HttpPost]
        public IActionResult CreateFilm([FromBody] CreateFilmModel film)
        {
            if (film == null)
            {
                return BadRequest();
            }
            _context.Films.Add(_mapper.Map<Film>(film));
            _context.SaveChanges();
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateFilm([FromBody] UpdateFilmModel film)
        {
            if (film == null)
            {
                return BadRequest();
            }
            var existingFilm = _context.Films.Find(film.Id);
            if (existingFilm == null)
            {
                return NotFound();
            }

            existingFilm.Name = film.Name;
            existingFilm.Genre = film.Genre;
            existingFilm.Director = film.Director;
            existingFilm.Duration = film.Duration;
            existingFilm.ReleaseDate = film.ReleaseDate;
            existingFilm.Restriction = film.Restriction;
            existingFilm.Description = film.Description;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilm(int id)
        {
            var film = _context.Films.FirstOrDefault(f => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            _context.Films.Remove(film);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
