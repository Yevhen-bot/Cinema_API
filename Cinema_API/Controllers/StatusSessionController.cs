using AutoMapper;
using Cinema_API.DTOs;
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
    public class StatusSessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public StatusSessionController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStatusSessions()
        {
            return Ok(_context.StatusSessions.ToList().Select(s => _mapper.Map<GetStatusSessionModel>(s)));
        }

        [HttpGet("{id}")]
        public IActionResult GetStatusSession(int id)
        {
            var statusSession = _context.StatusSessions.FirstOrDefault(s => s.Id == id);
            if (statusSession == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetStatusSessionModel>(statusSession));
        }

        [HttpPost]
        public IActionResult CreateStatusSession([FromBody] CreateStatusSessionModel statusSession)
        {
            if (statusSession == null)
            {
                return BadRequest();
            }
            _context.StatusSessions.Add(_mapper.Map<StatusSession>(statusSession));
            _context.SaveChanges();
            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatusSession(int id)
        {
            var statusSession = _context.StatusSessions.Find(id);
            if (statusSession == null)
            {
                return NotFound();
            }
            _context.StatusSessions.Remove(statusSession);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
