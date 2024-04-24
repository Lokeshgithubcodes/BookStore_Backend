using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;

namespace RepositoryLayer.Services
{
    public class CartRepository:ICartRepository
    {
        string connectionstring = "Data Source=LOKESH\\SQLEXPRESS;Initial Catalog=BookStoreDB;Integrated Security=True;";

        public List<Book> GetCartBooks(int UserId)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetCartByUserId_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<Book> list = new List<Book>();

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
                        list.Add(book);

                    }
                    return list;
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

        public List<Book> AddToCart(CartModel model, int UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddCart_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetCartBooks(UserId);
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

        public double GetPrice(int UserId)
        {
            List<Book> list = GetCartBooks(UserId);
            double totalPrice = 0;
            foreach (var book in list)
            {
                totalPrice += (book.Quantity * book.Price);
            }
            return totalPrice;
        }

        public CartModel UpdateQuantity(int UserId, CartModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("UpdateCart_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return null;

            }
        }

        public bool DeleteCart(DeleteCart model)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("DeleteCart_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);


                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return false;

            }
        }


    }
}
