using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CartBusiness:ICartBusiness
    {
        private readonly ICartRepository cartRepository;

        public CartBusiness(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public List<Book> GetCartBooks(int UserId)
        {
            return cartRepository.GetCartBooks(UserId);
        }

        public List<Book> AddToCart(CartModel model, int UserId)
        {
            return cartRepository.AddToCart(model, UserId);
        }

        public double GetPrice(int UserId)
        {
            return cartRepository.GetPrice(UserId);
        }

        public CartModel UpdateQuantity(int UserId, CartModel model)
        {
            return cartRepository.UpdateQuantity(UserId, model);
        }


        public bool DeleteCart(DeleteCart model)
        {
            return cartRepository.DeleteCart(model);
        }


    }
}
