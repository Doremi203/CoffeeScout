using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<Review> AddReview(Review review);
}