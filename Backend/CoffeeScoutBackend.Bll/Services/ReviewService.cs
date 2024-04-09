using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class ReviewService(
    IMenuItemService menuItemService,
    ICustomerService customerService,
    IReviewRepository reviewRepository
) : IReviewService
{
    public async Task<Review> Add(long menuItemId, string userId, Review review)
    {
        var menuItem = await menuItemService.GetById(menuItemId);
        var customer = await customerService.GetByUserId(userId);

        var reviewToAdd = review with
        {
            MenuItem = menuItem,
            Customer = customer
        };
        
        return await reviewRepository.AddReview(reviewToAdd);
    }

    public async Task<Review> GetById(long reviewId)
    {
        var review = await reviewRepository.GetById(reviewId)
            ?? throw new ReviewNotFoundException(
                $"Review with id: {reviewId} not found", reviewId);

        return review;
    }

    public async Task UpdateReview(long reviewId, Review review)
    {
        var existingReview = await GetById(reviewId);

        var updatedReview = review with
        {
            Id = reviewId,
            MenuItem = existingReview.MenuItem,
            Customer = existingReview.Customer
        };

        await reviewRepository.UpdateReview(updatedReview);
    }
    
    public async Task Delete(long reviewId)
    {
        var review = await GetById(reviewId);
        
        await reviewRepository.DeleteReview(review.Id);
    }
}