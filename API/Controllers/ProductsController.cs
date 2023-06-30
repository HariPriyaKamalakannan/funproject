using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly StoreContext _context;

        public ProductsController(ILogger<ProductsController> logger,StoreContext context)
        {
            _logger = logger;
            _context=context;
        }

       
        [HttpGet]
        public async Task<List<Product>> Getproducts()
        {
            var p=await _context.Products.ToListAsync();
            return  p;
        }
         [HttpGet("{id}")]
        public async Task<Product> Getproducts(int id)
        {
           var p=await _context.Products.FindAsync(id);
            return  p;
        }
    }
}