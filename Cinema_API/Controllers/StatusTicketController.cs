using AutoMapper;
using Cinema_API.DTOs;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_API.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTicketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public StatusTicketController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStatusTickets()
        {
            return Ok(_context.StatusTickets.ToList().Select(s => _mapper.Map<GetStatusTicketModel>(s)));
        }

        [HttpGet("{id}")]
        public IActionResult GetStatusTicket(int id)
        {
            var statusTicket = _context.StatusTickets.FirstOrDefault(s => s.Id == id);
            if (statusTicket == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetStatusTicketModel>(statusTicket));
        }

        [HttpPost]
        public IActionResult CreateStatusTicket([FromBody] CreateStatusTicketModel statusTicket)
        {
            if (statusTicket == null)
            {
                return BadRequest();
            }
            _context.StatusTickets.Add(_mapper.Map<StatusTicket>(statusTicket));
            _context.SaveChanges();
            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatusTicket(int id)
        {
            var statusTicket = _context.StatusTickets.Find(id);
            if (statusTicket == null)
            {
                return NotFound();
            }
            _context.StatusTickets.Remove(statusTicket);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
