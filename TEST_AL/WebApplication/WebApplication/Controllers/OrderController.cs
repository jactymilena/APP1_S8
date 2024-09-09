using WebApplication.Models;
using WebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        /// <summary>
        ///  Returns all the orders of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order List</returns>
        public ActionResult<List<Order>> GetAllOrders()
        {
            try
            {
                return Ok(_orderService.RetriveAllOrdersForCustomer());
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return NotFound("Order not found");
            }
        }

        [HttpGet("{orderId}")]
        /// <summary>
        ///  Returns the individual order of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order</returns>
        public ActionResult<Order> GetOrderById(int orderId)
        {
            try
            {
                return Ok(_orderService.RetriveSpecificOrderDetails(orderId));
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return NotFound("Order not found");
            }
        }

        [HttpPost]
        /// <summary>
        ///  Creates a new Order
        /// </summary>
        /// <param name="Order"></param>
        /// <returns>200</returns>
        public ActionResult<string> CreateOrder(Order order)
        {
            try
            {
                _orderService.PlaceOrder(order);
                return StatusCode(201, "Order Placed");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        /// <summary>
        ///  Modify the Order
        /// </summary>
        /// <param name="Order"></param>
        /// <returns>200</returns>
        public ActionResult<string> ModifyOrder(Order order)
        {
            try
            {
                _orderService.ModifyOrder(order);
                return Ok("Order Modified");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        /// <summary>
        ///  Modify the quantity of the food in order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="quantity"></param>
        /// <returns>200</returns>
        public ActionResult<string> ModifyFoodQuantity(int orderid, int quantity)
        {
            try
            {
                _orderService.ModifyFoodQuantityInOrder(orderid, quantity);
                return Ok("Quantity for the order has been Modifued");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        /// <summary>
        /// Cancel the order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>200</returns>
        public ActionResult<string> CancelOrder(int orderid)
        {
            try
            {
                _orderService.CancelOrder(orderid);
                return Ok("Order has cancelled");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

    }
}
