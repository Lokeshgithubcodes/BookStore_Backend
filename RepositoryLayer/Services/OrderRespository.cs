using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class OrderRespository:IOrderRespository
    {
        string connectionstring = "Data Source=LOKESH\\SQLEXPRESS;Initial Catalog=BookStoreDB;Integrated Security=True;";

        public List<Book> GetOrders(int userid)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetOrderByUserId_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<Book> orders = new List<Book>();

                    while (dataReader.Read())
                    {
                        Book book = new Book();
                        book.Id = Convert.ToInt32(dataReader["Id"]);
                        book.Title = dataReader["Title"].ToString();
                        book.Price = Convert.ToInt64(dataReader["Price"]);
                        book.Author = dataReader["Author"].ToString();
                        book.Description = dataReader["Description"].ToString();
                        book.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        book.Image = dataReader["Image"].ToString();
                        orders.Add(book);

                    }
                    return orders;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }


        public List<Book> AddToOrder(OrderModel model, int UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddOrder_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetOrders(UserId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }


        }


        public double GetPriceInOrder(int UserId)
        {
            List<Book> bookList = GetOrders(UserId);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.Quantity * book.Price);
            }
            return totalPrice;
        }

    }
}
