using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness business;

        public OrderController(IOrderBusiness business)
        {
            this.business = business;
        }

        [HttpGet]
        [Route("GetAllOrder")]
        public IActionResult Get_all_orders(int userid)
        {
            var data = business.GetOrders(userid);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddOrder")]

        public IActionResult Add_order(OrderModel model, int userid)
        {
            var data = business.AddToOrder(model, userid);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetOrderPrice")]

        public IActionResult Get_order_price(int userid)
        {
            var data = business.GetPriceInOrder(userid);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

    }
}
