using AutoMapper;
using Cinema_API.DTOs;
using Cinema_API.Jwt;
using Cinema_API.Services;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly CartService _cartservice;
        private readonly TicketService _ticketService;

        public TicketController(IMapper mapper, AppDbContext context, CartService cartService, TicketService ticketService)
        {
            _mapper = mapper;
            _context = context;
            _cartservice = cartService;
            _ticketService = ticketService;
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

        [Authorize(Policy = "Admin")]
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

        [Authorize(Policy = "Admin")]
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

        [HttpPut("return/{id}")]
        public IActionResult ReturnTicket(int id)
        {
            try
            {
                _ticketService.ReturnTicket(id);
            }
            catch (Exception e)
            {
                if(e.Message == "404")
                {
                    return NotFound();
                } else return BadRequest();
            }

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("addToCart/{id}")]
        public IActionResult AddTicketToCart(int id)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
                _cartservice.TicketBooked(id, userId);
            }
            catch (Exception e)
            {
                if (e.Message == "404")
                {
                    return NotFound();
                }
                else return BadRequest();
            }

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("removeFromCart/{id}")]
        public IActionResult RemoveTicketFromCart(int id)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
                _cartservice.TicketUnbooked(id, userId);
            }
            catch (Exception e)
            {
                if (e.Message == "404")
                {
                    return NotFound();
                }
                else return BadRequest();
            }

            _context.SaveChanges();
            return NoContent();
        }

        [Authorize(Policy = "Admin")]
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
