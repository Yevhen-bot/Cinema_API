using AutoMapper;
using Cinema_API.DTOs;
using Cinema_API.Services;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly TicketService _sessionService;

        public HallController(IMapper mapper, AppDbContext context, TicketService sessionService)
        {
            _mapper = mapper;
            _context = context;
            _sessionService = sessionService;
        }

        [HttpGet]
        public IActionResult GetHalls()
        {
            return Ok(_context.Halls.ToList().Select(h => _mapper.Map<GetUpdateHallModel>(h)));
        }

        [HttpGet("{id}")]
        public IActionResult GetHall(int id)
        {
            var hall = _context.Halls.FirstOrDefault(h => h.Id == id);
            if(hall == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUpdateHallModel>(hall));
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult CreateHall([FromBody] CreateHallModel hall)
        {
            if (hall == null)
            {
                return BadRequest();
            }
            _context.Halls.Add(_mapper.Map<Hall>(hall));
            _context.SaveChanges();
            return Created();
        }

        [Authorize(Policy = "Admin")]
        [HttpPut]
        public IActionResult UpdateHall([FromBody] GetUpdateHallModel hall)
        {
            if (hall == null)
            {
                return BadRequest();
            }
            var existingHall = _context.Halls.Find(hall.Id);
            if (existingHall == null)
            {
                return NotFound();
            }
            _sessionService.HallUpdated(existingHall.Id, hall.Seats);
            existingHall.Seats = hall.Seats;
            existingHall.IsVip = hall.IsVip;

            _context.SaveChanges();
            return Ok();
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteHall(int id)
        {
            var hall = _context.Halls.FirstOrDefault(h => h.Id == id);
            if (hall == null)
            {
                return NotFound();
            }
            _context.Halls.Remove(hall);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
