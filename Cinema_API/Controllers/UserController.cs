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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UserController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_context.Users.ToList().Select(u => _mapper.Map<GetUpdateUserModel>(u)));
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUpdateUserModel>(user));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            _context.Users.Add(_mapper.Map<User>(user));
            _context.SaveChanges();
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] GetUpdateUserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var existingUser = _context.Users.Find(user);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;  
            existingUser.IsAdmin = user.IsAdmin;
            existingUser.Bonuses = user.Bonuses;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
