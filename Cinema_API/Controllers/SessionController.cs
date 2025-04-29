using AutoMapper;
using Cinema_API.DTOs;
using Cinema_API.Services;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly SessionService _sessionService;

        public SessionController(IMapper mapper, AppDbContext context, SessionService sessionService)
        {
            _mapper = mapper;
            _context = context;
            _sessionService = sessionService;
        }

        [HttpGet]
        public IActionResult GetSessions()
        {
            return Ok(_context.Sessions.ToList().Select(s => _mapper.Map<GetUpdateSessionModel>(s)));
        }

        [HttpGet("{id}")]
        public IActionResult GetSession(int id)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUpdateSessionModel>(session));
        }

        [HttpPost]
        public IActionResult CreateSession([FromBody] CreateSessionModel session)
        {
            if (session == null)
            {
                return BadRequest();
            }
            var s = _mapper.Map<Session>(session);
            _context.Sessions.Add(s);
            _context.SaveChanges();
            _sessionService.SessionCreated(s);
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateSession([FromBody] GetUpdateSessionModel session)
        {
            if (session == null)
            {
                return BadRequest();
            }
            var existingSession = _context.Sessions.Find(session.Id);
            if (existingSession == null)
            {
                return NotFound();
            }

            existingSession.FilmId = session.FilmId;
            existingSession.HallId = session.HallId;
            existingSession.StartTime = session.StartTime;
            existingSession.Price = session.Price;
            existingSession.StatusId = session.StatusId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSession(int id)
        {
            var session = _context.Sessions.Find(id);
            if (session == null)
            {
                return NotFound();
            }
            _context.Sessions.Remove(session);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
