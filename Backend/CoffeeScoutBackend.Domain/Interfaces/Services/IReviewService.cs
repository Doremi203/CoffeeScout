using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IReviewService
{
    Task<Review> AddReview(long menuItemId, string userId, Review review);
}