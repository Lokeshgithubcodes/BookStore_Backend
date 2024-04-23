using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookBusiness bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
           this.bookBusiness = bookBusiness;
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult Get()
        {
            var data = bookBusiness.GetAllBooks();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult Get_byid(int id)
        {
            var data = bookBusiness.GetBookById(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("GetByTitle")]
        public IActionResult Get_by_title(string Title)
        {
            var data = bookBusiness.GetBookByTitle(Title);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("GetByAuthor")]
        public IActionResult Get_by_Author(string Author)
        {
            var data = bookBusiness.GetBookByAuthor(Author);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpGet]
        [Route("GetByTitleAndAuthor")]
        public IActionResult Get_by_title_and_author(string title, string author)
        {
            var data = bookBusiness.GetBookByTitleAndAuthor(title, author);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


    }
}
