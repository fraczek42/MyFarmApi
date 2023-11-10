using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyFarmApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOrders")]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                var orders = GetSampleOrders();

                if (orders == null || !orders.Any())
                {
                    return NotFound("No orders found.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching orders from the database.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }


        private List<Order> GetSampleOrders()
        {
            return new List<Order>
            {
                new Order { OrderingPersonId = 1, Quantity = 5, DateOrder = DateTime.Now.AddDays(-5) }
            };
        }
    }
}

public class Order
{
    public int Id { get; set; }
    public int OrderingPersonId { get; set; }
    public int Quantity { get; set; }
    public int QuantityPerPackage { get; set; }
    public int OrderedGoodsId { get; set; }
    public decimal AmountPerQuantity { get; set; }
    public DateTime DateOrder { get; set; }
    public int Status { get; set; }
}