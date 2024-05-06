using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWhishListBusiness whishListBusiness;

        public WishListController(IWhishListBusiness whishListBusiness)
        {
            this.whishListBusiness = whishListBusiness;
        }

        [HttpGet]
        [Route("GetWhishList")]

        public IActionResult get_wishlist(int userid)
        {
            var data = whishListBusiness.GetWhishListBooks(userid);

            if (data == null)
            {
                return BadRequest();
            }

            return Ok(new {Success=true,Message="wishlist items are",Data=data});
        }

        [HttpPost]
        [Route("AddToWishList")]

        public IActionResult Add_whishlist(AddWhishlist model)
        {
            var data = whishListBusiness.AddToWishList(model);

            if (data == null)
            {
                return BadRequest();
            }
            return Ok(new {success=true, Message="Added Succesfully", Data=data});
        }


        [HttpPost]
        [Route("DeleteWhishList")]

        public IActionResult delete_whishlist(DeleteCart model)
        {
            var data = whishListBusiness.DeleteWhishlist(model);

            if (data == null)
            {
                return BadRequest(new { success = false, message = "Someting error" });
            }

            return Ok(new { message = "deleted sucessfully", result = true });
        }
    }
}
