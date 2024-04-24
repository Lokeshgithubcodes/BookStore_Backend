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
    public class ReviewBusiness:IReviewBusiness
    {
        private readonly IReviewRespository reviewRespository;

        public ReviewBusiness(IReviewRespository reviewRespository)
        {
            this.reviewRespository = reviewRespository;
        }

        public ReviewModel AddReview(ReviewModel model, int UserId)
        {
            return reviewRespository.AddReview(model, UserId);
        }

        public List<Review> GetReviews(int Id)
        {
            return reviewRespository.GetReviews(Id);
        }
    }
}
