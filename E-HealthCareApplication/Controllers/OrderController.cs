using E_HealthCareApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_HealthCareApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly HealthCareDbContext _context;
        public OrderController(HealthCareDbContext context)
        {
            _context = context;
        }

        [HttpGet("getOrderByUserId/userId-{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Order>> GetOrderByUserId(int userId)
        {
            var order = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();

            if (order is null)
                return BadRequest("Order not found.");

            if (order.Count() <= 0)
                return BadRequest("No order placed");

            return Ok(order);
        }

        [HttpPut("PlaceTheOrder/userId-{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Order>> PlaceTheOrder(int userId)
        {
            var carts = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();

            var account = await _context.Accounts.FindAsync(userId);
            double price = 0;

            foreach (var cart in carts)
            {
                price += cart.TotalPrice;
            }

            string productNames = "";
            var order = new Order();
            foreach (var cart in carts)
            {
                _context.Carts.Remove(cart);
                var product = await _context.Products.FindAsync(cart.ProductId);
                if (product is not null)
                {
                    productNames += product.Name;
                }

                order.TotalPrice += cart.TotalPrice;
            }

            order.ProductNames = productNames;
            order.UserId = userId;
            order.Status = "Processing";
            order.DateTime = DateTime.UtcNow;

            account = await _context.Accounts.FindAsync(userId);
            if (account is not null)
            {
                _context.Entry(account).State = EntityState.Modified;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}
