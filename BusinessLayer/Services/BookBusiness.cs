using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookBusiness:IBookBusiness
    {
        private readonly IBookRespository bookRespository;

        public BookBusiness(IBookRespository bookRespository)
        {
            this.bookRespository = bookRespository;
        }

        public List<Book> GetAllBooks()
        {
            return bookRespository.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return bookRespository.GetBookById(id);
        }

        public Book GetBookByTitle(string Title)
        {
            return bookRespository.GetBookByTitle(Title);
        }

        public Book GetBookByAuthor(string Author)
        {
            return bookRespository.GetBookByAuthor(Author);
        }

        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            return bookRespository.GetBookByTitleAndAuthor(title, author);
        }
    }
}
