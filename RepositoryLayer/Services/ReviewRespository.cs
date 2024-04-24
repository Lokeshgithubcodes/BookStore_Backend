using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Services
{
    public class ReviewRespository:IReviewRespository
    {
        string connectionstring = "Data Source=LOKESH\\SQLEXPRESS;Initial Catalog=BookStoreDB;Integrated Security=True;";

        public ReviewModel AddReview(ReviewModel model, int UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddReview_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Reviews", model.Reviews);
                    cmd.Parameters.AddWithValue("@Star", model.Star);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
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

        public List<Review> GetReviews(int Id)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetReviewsForBook_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", Id);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<Review> reviews = new List<Review>();

                    while (dataReader.Read())
                    {
                        Review review = new Review();
                        review.FullName = dataReader["FullName"].ToString();
                        review.Reviews = dataReader["Review"].ToString();
                        review.Star = Convert.ToInt32(dataReader["Star"]);
                        review.Id = Convert.ToInt32(dataReader["BookId"]);

                        reviews.Add(review);

                    }
                    return reviews;
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


    }
}
