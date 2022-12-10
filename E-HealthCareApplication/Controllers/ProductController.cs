using E_HealthCareApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_HealthCareApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly HealthCareDbContext _context;
        public ProductController(HealthCareDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllProducts")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("getProductById/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
                return BadRequest("Product not found.");

            return Ok(product);
        }

        [HttpPost("addProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Product is added successfully");
        }

        [HttpPut("updateProduct/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            product.Id = id;
            product.Name = product.Name;
            product.Description = product.Description;
            product.ExpireDate = product.ExpireDate;
            product.Quantity = product.Quantity;
            product.Price = product.Price;
            product.CompanyName = product.CompanyName;

            _context.Entry(product).State = EntityState.Modified;

            _context.Update(product);
            await _context.SaveChangesAsync();

            return Ok("Product is updated successfully");
        }

        [HttpDelete("deleteProduct/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Product>>> DeleteProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return BadRequest("Product not found.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("The selected product has been deleted");
        }
    }
}
