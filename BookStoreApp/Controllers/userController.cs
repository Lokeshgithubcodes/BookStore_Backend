using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        
        

        public userController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
          
           
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register_User(UserModel model)
        {
            var registerdata = userBusiness.RegisterUser(model);
            if (registerdata != null)
            {
                return Ok(new { Succes = true, Message = "Login Sucessfull", Data = registerdata });
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult get_data()
        {
            var data = userBusiness.GetData();
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateUser")]

        public IActionResult update_user(int id, UpdateUser model)
        {
            var data = userBusiness.UpdateUser(id, model);
            if (data != null)
            {
                return Ok(data);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("Login")]
        public IActionResult Login_user(LoginModel model)
        {
            var logindata = userBusiness.LoginUser(model);
            if (logindata != null)
            {
                return Ok(new {Succes=true,Message="Login Sucessfull",Data= logindata });
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> Forgot_PasswordAsync(string email)
        {
            var password = userBusiness.ForgotPassword(email);
            if (password != null)
            {
                Sendmsg send = new Sendmsg();

                send.SendMessage(password.EmailId, "Password is Trying to Changed is that you....!\nToken: " + password.token);

                Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                //var endPoint = await bus.GetSendEndpoint(uri);
                //await endPoint.Send(ticket);


                return Ok("User Found" + "\n Token:" + password.token);

            }
            return BadRequest("User Not Found");
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult reset_password(string password)
        {
            var userid = User.Claims.Where(x => x.Type == "Email").FirstOrDefault().Value;

            var data = userBusiness.ResetPassword(userid, password);
            if (data != null)
            {
                return Ok("Password Changed Sucessfully");
            }
            return BadRequest("Invalid Credentials");

        }

        [HttpPost]
        [Route("InsertOrUpdate")]

        public IActionResult InserOrUpdate(User user)
        {
            var res=userBusiness.UserInsertOrUpdate(user);
            if (res != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }
    }
}
