using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IWhishListBusiness
    {
        public List<Book> GetWhishListBooks(int userid);

        public List<Book> AddToWishList(AddWhishlist model);

        public bool DeleteWhishlist(DeleteCart model);
    }
}
