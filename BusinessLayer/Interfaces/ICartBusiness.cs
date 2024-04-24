using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICartBusiness
    {
        public List<Book> GetCartBooks(int UserId);

        public List<Book> AddToCart(CartModel model, int UserId);

        public double GetPrice(int UserId);

        public CartModel UpdateQuantity(int UserId, CartModel model);

        public bool DeleteCart(DeleteCart model);

    }
}
