using E_HealthCareApplication.Domain;
using E_HealthCareApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_HealthCareApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly HealthCareDbContext _context;
        public CartController(HealthCareDbContext context)
        {
            _context = context;
        }

        [HttpGet("getCartByUserId/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<Cart>>> GetCartByUserId(int userId)
        {
            var cart = await _context.Carts.Where(c => c.UserId == userId).ToArrayAsync();
            return Ok(cart.Length == 0 ? 404 : cart);
        }

        [HttpPut("addToCart/userId-{userId}/productId-{productId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Cart>> AddToCart(CartRequest addToCart)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == addToCart.ProductId);
            var userAccount = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == addToCart.UserId);

            var cart = await _context.Carts.Where(u => u.UserId == addToCart.UserId).FirstOrDefaultAsync(p => p.ProductId == addToCart.ProductId);

            if (product is null || userAccount is null)
                return BadRequest("User or Product not found");

            if (product.Quantity <= 0)
                return BadRequest("Atleast one product must be required to add to cart");

            if (cart is null)
            {
                var newdata = new Cart
                {
                    UserId = addToCart.UserId,
                    ProductId = addToCart.ProductId,
                    Quantity = addToCart.Quantity,
                    TotalPrice = product.Price * addToCart.Quantity
                };

                product.Quantity -= addToCart.Quantity;

                await _context.Carts.AddAsync(newdata);
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(newdata);
            }

            _context.Entry(cart).State = EntityState.Modified;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        [HttpPut("removeFromCart/userId-{userId}/productId-{productId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Cart>> RemoveFromCart(CartRequest cartRequest)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartRequest.ProductId);
            var userAccount = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == cartRequest.UserId);

            var cart = await _context.Carts.Where(u => u.UserId == cartRequest.UserId).FirstOrDefaultAsync(p => p.ProductId == cartRequest.ProductId);

            if (product is null || userAccount is null)
                return BadRequest("User and Product not found.");

            if (cart == null)
                return BadRequest("Cart is empty");

            cart.TotalPrice -= product.Price * cartRequest.Quantity;
            cart.Quantity -= cartRequest.Quantity;
            product.Quantity += cartRequest.Quantity;

            if (cart.Quantity == 0)
                _context.Carts.Remove(cart);

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(cart);
        }
    }
}
