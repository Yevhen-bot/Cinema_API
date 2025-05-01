using AutoMapper;
using Cinema_API.DTOs;
using Cinema_API.Services;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly CartService _cartservice;

        public SaleController(IMapper mapper, AppDbContext context, CartService cartservice)
        {
            _mapper = mapper;
            _context = context;
            _cartservice = cartservice;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult GetSales()
        {
            return Ok(_context.Sales.Include(s => s.Tickets).ToList().Select(s => _mapper.Map<GetUpdateSaleModel>(s)));
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetSale(int id)
        {
            var sale = _context.Sales.Include(s => s.Tickets).FirstOrDefault(s => s.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUpdateSaleModel>(sale));
        }

        [HttpPost("order")]
        public IActionResult CreateOrder()
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
                _cartservice.CartBought(userId);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Created();
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id) 
        {
            var sale = _context.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }
            _context.Sales.Remove(sale);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("clearCart")]
        public IActionResult ClearCart()
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
                _cartservice.AbortUserSession(userId);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
