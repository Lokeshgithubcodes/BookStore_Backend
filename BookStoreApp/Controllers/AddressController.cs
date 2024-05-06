using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressbusiness business;

        public AddressController(IAddressbusiness business)
        {
            this.business = business;
        }

        [HttpPost]
        [Route("AddAddress")]

        public IActionResult add_address(AddressModel model)
        {
            var data = business.AddAddress(model);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();

        }

        [HttpGet]
        [Route("GetAddressByUserId")]

        public IActionResult get_address(int userid)
        {
            var data = business.GetAddresses(userid);
            if (data != null)
            {
                return Ok(new {success=true, Message="Successfull", Data=data});
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("UpdateAddress")]

        public IActionResult update_address(AddressUpdateModel model)
        {
            var data = business.UpdateAddress(model);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }


    }
}
