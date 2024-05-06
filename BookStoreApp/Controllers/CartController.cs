using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness cartBusiness;

        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }

        [HttpGet]
        [Route("GetCardByUserId")]
        public IActionResult GetCard(int userid)
        {
            var data = cartBusiness.GetCartBooks(userid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("AddToCartByUserId")]
        public IActionResult AddCart(int userid, CartModel model)
        {
            var data = cartBusiness.AddToCart(model, userid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new {Success=true, Message="Added Successfull", Data=data});
        }

        [HttpGet]
        [Route("GetCardPrice")]
        public IActionResult GetCardPrice(int userid)
        {
            var data = cartBusiness.GetPrice(userid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new { Success = true, Message = "Successfull", Data = data });
        }

        [HttpPut]
        [Route("UpdateQuantityByUserId")]
        public IActionResult UpdateQty(int userid, CartModel model)
        {
            var data = cartBusiness.UpdateQuantity(userid, model);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new { Success = true, Message = "Successfull", Data = data });
        }


        [HttpPost]
        [Route("DeleteCart")]

        public IActionResult Delete_cart(DeleteCart model)
        {
            var data = cartBusiness.DeleteCart(model);
            if (!data)
            {
                return NotFound("Cart Not found");
            }
            return Ok(new { message = "deleted sucessfully", result = true });
        }

    }
}
