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
    public class WhishListRepository:IWhishListRepository
    {
        string connectionstring = "Data Source=LOKESH\\SQLEXPRESS;Initial Catalog=BookStoreDB;Integrated Security=True;";

        public List<Book> GetWhishListBooks(int userid)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetWishListByUserId_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

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

        public List<Book> AddToWishList(AddWhishlist model)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddWishList_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);
                    cmd.Parameters.AddWithValue("@BookId", model.Bookid);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetWhishListBooks(model.Id);
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


        public bool DeleteWhishlist(DeleteCart model)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("deleteWishList_sp", conn);
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
