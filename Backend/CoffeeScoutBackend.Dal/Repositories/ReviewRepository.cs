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

    public async Task<Review?> GetById(long reviewId)
    {
        var reviewEntity = await dbContext.Reviews
            .Include(r => r.Customer)
            .Include(r => r.MenuItem)
            .FirstOrDefaultAsync(r => r.Id == reviewId);
        
        return reviewEntity?.Adapt<Review>();
    }

    public async Task<IReadOnlyCollection<Review>> GetByMenuItemId(long menuItemId)
    {
        var reviews = await dbContext.Reviews
            .Include(r => r.Customer)
            .Include(r => r.MenuItem)
            .Where(r => r.MenuItem.Id == menuItemId)
            .ToListAsync();
        
        return reviews.Adapt<IReadOnlyCollection<Review>>();
    }

    public async Task UpdateReview(Review updatedReview)
    {
        var reviewEntity = await dbContext.Reviews
            .FirstAsync(r => r.Id == updatedReview.Id);
        
        reviewEntity.Rating = updatedReview.Rating;
        reviewEntity.Content = updatedReview.Content;
        
        dbContext.Reviews.Update(reviewEntity);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteReview(long reviewId)
    {
        var reviewEntity = await dbContext.Reviews
            .FirstAsync(r => r.Id == reviewId);
        
        dbContext.Reviews.Remove(reviewEntity);
        
        await dbContext.SaveChangesAsync();
    }
}