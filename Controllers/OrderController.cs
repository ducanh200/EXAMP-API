using EXAMP.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EXAMP.Models;

namespace EXAMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder( [FromBody] CreateModel orderModel)
        {

            if (orderModel == null)
            {
                return BadRequest("Invalid order data.");
            }

            var order = new Order
            {
                ItemCode = orderModel.ItemCode,
                ItemName = orderModel.ItemName,
                ItemQty = orderModel.ItemQty,
                OrderDelivery = orderModel.OrderDelivery,
                OrderAddress = orderModel.OrderAddress,
                PhoneNumber = orderModel.PhoneNumber
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }


        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

    }
}
