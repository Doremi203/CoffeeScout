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
    public async Task<Review> AddReview(long menuItemId, string userId, Review review)
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
}