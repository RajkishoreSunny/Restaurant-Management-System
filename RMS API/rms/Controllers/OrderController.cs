using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.OrdersModel;
using Services.OrderSvc;
namespace Controller.OrderCtrl
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("ListOfOrders")]
        [Authorize]
        public ActionResult<Orders> ListOfOrders(int OrderId)
        {
            try
            {
                var orders = _orderService.ListOfOrders(OrderId);
                if(orders != null)
                {
                    return Ok(orders);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddOrder")]
        [Authorize]
        public ActionResult<Orders> AddOrder([FromBody] Orders order)
        {
            try
            {
                var orderToAdd = _orderService.AddOrder(order);
                if(orderToAdd != null)
                {
                    return Ok(order);
                }
                return NotFound();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("GetListOfOrdersForCustomer")]
        [Authorize]
        public ActionResult<List<Orders>> GetListOfOrdersForCustomer(int CustomerId)
        {
            try
            {
                var orderList = _orderService.GetListOfOrdersForCustomer(CustomerId);
                if(orderList != null)
                {
                    return Ok(orderList);
                }
                return NotFound();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("SendOrderInvoiceEmail")]
        public async Task<ActionResult<bool>> SendOrderInvoiceEmail(IFormFile formFile, [FromForm]OrdersDto orders)
        {
            try
            {
                var response = await _orderService.SendOrderInvoiceEmail(orders, formFile);
                if(response == true)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}