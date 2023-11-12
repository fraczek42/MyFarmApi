using Microsoft.AspNetCore.Mvc;
using System.Net;
using MyFarmApi.Models;

namespace MyFarmApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        // GET /api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
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
                _logger.LogError(ex, "Error while fetching orders.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        // GET /api/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            try
            {
                var order = GetSampleOrders().FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching order with ID {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        // POST /api/orders
        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order newOrder)
        {
            try
            {
                // Logika dodawania nowego zamówienia
                // ...

                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new order.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        // PUT /api/orders/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                if (id != order.Id)
                {
                    return BadRequest("Order ID mismatch.");
                }

                // Logika aktualizacji zamówienia
                // ...

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating order with ID {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        // DELETE /api/orders/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                // Logika usuwania zamówienia
                // ...

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting order with ID {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        private List<Order> GetSampleOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    OrderingPersonId = 1,
                    Quantity = 5,
                    DateOrder = DateTime.Now.AddDays(-5),
                    AmountPerQuantity = 40,
                    QuantityPerPackage = 25,
                    OrderedGoodsId = 1,
                    Status = 1
                },
                new Order
                {
                    Id = 2,
                    OrderingPersonId = 2,
                    Quantity = 30,
                    DateOrder = DateTime.Now.AddDays(-10),
                    AmountPerQuantity = 35,
                    QuantityPerPackage = 20,
                    OrderedGoodsId = 2,
                    Status = 2
                },
                new Order
                {
                    Id = 3,
                    OrderingPersonId = 3,
                    Quantity = 22,
                    DateOrder = DateTime.Now.AddDays(-3),
                    AmountPerQuantity = 45,
                    QuantityPerPackage = 30,
                    OrderedGoodsId = 3,
                    Status = 1
                },
                new Order
                {
                    Id = 4,
                    OrderingPersonId = 2,
                    Quantity = 30,
                    DateOrder = DateTime.Now.AddDays(-7),
                    AmountPerQuantity = 50,
                    QuantityPerPackage = 10,
                    OrderedGoodsId = 4,
                    Status = 3
                },
                new Order
                {
                    Id = 5,
                    OrderingPersonId = 3,
                    Quantity = 30,
                    DateOrder = DateTime.Now.AddDays(-1),
                    AmountPerQuantity = 60,
                    QuantityPerPackage = 5,
                    OrderedGoodsId = 5,
                    Status = 2
                }
            };
        }
    }
}
