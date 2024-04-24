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
    public class OrderBusiness:IOrderBusiness
    {
        private readonly IOrderRespository orderRespository;

        public OrderBusiness(IOrderRespository orderRespository)
        {
            this.orderRespository = orderRespository;
        }

        public List<Book> GetOrders(int userid)
        {
            return orderRespository.GetOrders(userid);
        }

        public List<Book> AddToOrder(OrderModel model, int UserId)
        {
            return orderRespository.AddToOrder(model, UserId);
        }

        public double GetPriceInOrder(int UserId)
        {
            return orderRespository.GetPriceInOrder(UserId);
        }
    }
}
