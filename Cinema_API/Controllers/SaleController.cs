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
    public class SaleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public SaleController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            return Ok(_context.Sales.ToList().Select(s => _mapper.Map<GetUpdateSaleModel>(s)));
        }

        [HttpGet("{id}")]
        public IActionResult GetSale(int id)
        {
            var sale = _context.Sales.FirstOrDefault(s => s.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUpdateSaleModel>(sale));
        }

        [HttpPost]
        public IActionResult CreateSale([FromBody] CreateSaleModel sale)
        {
            if (sale == null)
            {
                return BadRequest();
            }
            _context.Sales.Add(_mapper.Map<Sale>(sale));
            _context.SaveChanges();
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateSale([FromBody] GetUpdateSaleModel sale)
        {
            if (sale == null)
            {
                return BadRequest();
            }
            var existingSale = _context.Sales.Find(sale.Id);
            if (existingSale == null)
            {
                return NotFound();
            }
            _mapper.Map(sale, existingSale);
            _context.SaveChanges();
            return NoContent();
        }

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
    }
}
