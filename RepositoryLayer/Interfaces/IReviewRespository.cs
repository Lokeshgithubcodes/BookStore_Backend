using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IReviewRespository
    {
        public ReviewModel AddReview(ReviewModel model, int UserId);

        public List<Review> GetReviews(int Id);
    }
}
