using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class ReviewRepository(
    AppDbContext dbContext
) : IReviewRepository
{
    public async Task<Review> AddReview(Review review)
    {
        var reviewEntity = review.Adapt<ReviewEntity>();
        reviewEntity.Customer = await dbContext.Customers
            .FirstAsync(c => c.Id == review.Customer.Id);
        reviewEntity.MenuItem = await dbContext.MenuItems
            .FirstAsync(mi => mi.Id == review.MenuItem.Id);
        
        await dbContext.Reviews.AddAsync(reviewEntity);
        await dbContext.SaveChangesAsync();
        
        return reviewEntity.Adapt<Review>();
    }
}