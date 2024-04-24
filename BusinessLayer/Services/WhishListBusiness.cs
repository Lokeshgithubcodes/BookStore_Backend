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
    public class WhishListBusiness:IWhishListBusiness
    {
        private readonly IWhishListRepository whishListRepository;
        
        public WhishListBusiness(IWhishListRepository whishListRepository)
        {
            this.whishListRepository = whishListRepository;
        }

        public List<Book> GetWhishListBooks(int userid)
        {
            return whishListRepository.GetWhishListBooks(userid);
        }

        public List<Book> AddToWishList(AddWhishlist model)
        {
            return whishListRepository.AddToWishList(model);
        }

        public bool DeleteWhishlist(DeleteCart model)
        {
            return whishListRepository.DeleteWhishlist(model);
        }
    }
}
