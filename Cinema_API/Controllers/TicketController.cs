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
    public class TicketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public TicketController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTickets()
        {
            return Ok(_context.Tickets.ToList().Select(t => _mapper.Map<GetUpdateTicketModel>(t)));
        }

        [HttpGet("{id}")]
        public IActionResult GetTicket(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUpdateTicketModel>(ticket));
        }

        [HttpPost]
        public IActionResult CreateTicket([FromBody] CreateTicketModel ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }
            _context.Tickets.Add(_mapper.Map<Ticket>(ticket));
            _context.SaveChanges();
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateTicket([FromBody] GetUpdateTicketModel ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }
            var existingTicket = _context.Tickets.Find(ticket.Id);
            if (existingTicket == null)
            {
                return NotFound();
            }
            _mapper.Map(ticket, existingTicket);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
