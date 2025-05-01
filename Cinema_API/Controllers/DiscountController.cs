using AutoMapper;
using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cinema_API.DTOs;
using DataAccess.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Cinema_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public DiscountController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDiscounts()
        {
            return Ok(_context.Discounts.ToList().Select(d => _mapper.Map<GetDiscountModel>(d)));
        }

        [HttpGet("{id}")]
        public IActionResult GetDiscount(int id)
        {
            var discount = _context.Discounts.FirstOrDefault(d => d.Id == id);
            if (discount == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetDiscountModel>(discount));
        }

        [Authorize(Policy = "Admin")]
        [HttpPost] 
        public IActionResult CreateDiscount([FromBody] CreateDiscountModel discount)
        {
            if (discount == null)
            {
                return BadRequest();
            }
            _context.Discounts.Add(_mapper.Map<Discount>(discount));
            _context.SaveChanges();
            return Created();
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var discount = _context.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }
            _context.Discounts.Remove(discount);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
