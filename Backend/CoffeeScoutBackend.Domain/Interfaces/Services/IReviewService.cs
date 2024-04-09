using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IReviewService
{
    Task<Review> Add(long menuItemId, string userId, Review review);
    Task<Review> GetById(long reviewId);
    Task UpdateReview(long reviewId, Review review);
    Task Delete(long reviewId);
}