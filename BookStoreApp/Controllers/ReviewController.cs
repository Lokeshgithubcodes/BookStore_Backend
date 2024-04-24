using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewBusiness reviewBusiness;

        public ReviewController(IReviewBusiness reviewBusiness)
        {
            this.reviewBusiness = reviewBusiness;
        }

        [HttpPost]
        [Route("AddReviewByUserId")]
        public IActionResult Add_review(ReviewModel model, int userid)
        {
            var data = reviewBusiness.AddReview(model, userid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("GetReviewByBookId")]
        public IActionResult Getreview(int Id)
        {
            var data = reviewBusiness.GetReviews(Id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
