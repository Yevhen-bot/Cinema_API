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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly CartService _cartservice;

        public UserController(IMapper mapper, AppDbContext context, CartService cartService)
        {
            _mapper = mapper;
            _context = context;
            _cartservice = cartService;
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
            var u = _mapper.Map<User>(user);
            _context.Users.Add(u);
            _context.SaveChanges();
            _cartservice.UserCreated(u.Id);

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
