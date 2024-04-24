using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBusiness
    {
        public List<Book> GetOrders(int userid);

        public List<Book> AddToOrder(OrderModel model, int UserId);

        public double GetPriceInOrder(int UserId);
    }
}
