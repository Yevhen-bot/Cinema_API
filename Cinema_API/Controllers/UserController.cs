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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly CartService _cartservice;
        private readonly JwtProvider _jwtProvider;

        public UserController(IMapper mapper, AppDbContext context, CartService cartService, JwtProvider jwtProvider)
        {
            _mapper = mapper;
            _context = context;
            _cartservice = cartService;
            _jwtProvider = jwtProvider;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_context.Users.ToList().Select(u => _mapper.Map<GetUpdateUserModel>(u)));
        }

        [Authorize(Policy = "Admin")]
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

        [HttpPost("register")]
        public IActionResult Sign([FromBody] CreateUserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var u = _mapper.Map<User>(user);
            u.IsAdmin = false;
            _context.Users.Add(u);
            _context.SaveChanges();
            u.CartId = _cartservice.UserCreated(u.Id);
            _context.Users.Update(u);
            _context.SaveChanges();

            return Created();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUser user)
        {
            var us = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (us == null)
            {
                return NotFound();
            }
            var token = _jwtProvider.GenerateToken(us);

            HttpContext.Response.Cookies.Append("name", token);

            return Ok();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("addworker")]
        public IActionResult AddWorker([FromBody] CreateAdminModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var u = _mapper.Map<User>(user);
            u.IsAdmin = true;
            _context.Users.Add(u);
            _context.SaveChanges();
            u.CartId = _cartservice.UserCreated(u.Id);
            _context.Users.Update(u);
            _context.SaveChanges();

            return Created();
        }

        [Authorize(Policy = "Admin")]
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

        [Authorize]
        [HttpPut("changeEmail")]
        public IActionResult ChangeEmail([FromBody] ChangeEmailModel emails)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(HttpContext.User.FindFirst("userId").Value));
            if(user == null)
            {
                return NotFound();
            }

            if(user.Email != emails.OldEmail)
            {
                return BadRequest();
            }

            user.Email = emails.NewEmail;
            _context.SaveChanges();

            return NoContent();
        }


        [Authorize(Policy = "Admin")]
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
