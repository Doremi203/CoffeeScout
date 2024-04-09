using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IReviewService
{
    Task<Review> AddReview(Review review);
}