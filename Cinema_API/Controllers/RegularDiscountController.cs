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
    public class RegularDiscountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public RegularDiscountController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRegularDiscounts()
        {
            return Ok(_context.RegularDiscounts.ToList().Select(d => _mapper.Map<GetRegularDiscountModel>(d)));
        }

        [HttpGet("{id}")]
        public IActionResult GetRegularDiscount(int id)
        {
            var discount = _context.RegularDiscounts.FirstOrDefault(d => d.Id == id);
            if (discount == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetRegularDiscountModel>(discount));
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult CreateRegularDiscount([FromBody] CreateRegularDiscountModel discount)
        {
            if (discount == null)
            {
                return BadRequest();
            }
            _context.RegularDiscounts.Add(_mapper.Map<RegularDiscount>(discount));
            _context.SaveChanges();
            return Created();
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRegularDiscount(int id)
        {
            var discount = _context.RegularDiscounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }
            _context.RegularDiscounts.Remove(discount);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
