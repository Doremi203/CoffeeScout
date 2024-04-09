using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<Review> AddReview(Review review);
    Task<Review?> GetById(long reviewId);
    Task<IReadOnlyCollection<Review>> GetByMenuItemId(long menuItemId);
    Task UpdateReview(Review updatedReview);
    Task DeleteReview(long reviewId);
}