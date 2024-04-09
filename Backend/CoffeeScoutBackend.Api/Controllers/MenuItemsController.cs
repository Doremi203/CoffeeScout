using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.MenuItems)]
public class MenuItemsController(
    IMenuItemService menuItemService,
    ICafeService cafeService,
    IReviewService reviewService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<List<MenuItemResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMenuItemsByBeverageTypeInArea(
        [FromQuery] GetMenuItemsByBeverageTypeInAreaRequest request
    )
    {
        var menuItems = await menuItemService.GetAllInAreaByBeverageType(
            new Location
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            request.RadiusInMeters,
            request.BeverageTypeId);

        return Ok(menuItems.Adapt<IEnumerable<MenuItemResponse>>());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddMenuItem(AddMenuItemRequest request)
    {
        var newMenuItem = new MenuItem
        {
            Name = request.Name,
            Price = request.Price,
            SizeInMl = request.SizeInMl,
            BeverageType = new BeverageType
            {
                Name = request.BeverageTypeName
            }
        };
        var menuItem = await menuItemService.Add(User.GetId(), newMenuItem);

        return Created($"{RoutesV1.MenuItems}/{menuItem.Id}", menuItem.Adapt<MenuItemResponse>());
    }
    
    [HttpPatch("{id:long}")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMenuItem(long id, UpdateMenuItemRequest request)
    {
        if (!await IsMenuItemInCafe(id))
            return Forbid();
        
        var menuItem = new MenuItem
        {
            Id = id,
            Name = request.Name,
            Price = request.Price,
            SizeInMl = request.SizeInMl,
            BeverageType = new BeverageType
            {
                Name = request.BeverageTypeName
            }
        };
        await menuItemService.Update(id, menuItem);

        return NoContent();
    }
    
    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteMenuItem(long id)
    {
        if (!await IsMenuItemInCafe(id))
            return Forbid();
        
        await menuItemService.Delete(id);

        return NoContent();
    }
    
    [HttpPost("{menuItemId:long}/reviews")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<ReviewResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddReview(long menuItemId, AddReviewRequest request)
    {
        var reviewToAdd = new Review
        {
            Rating = request.Rating,
            Content = request.Content
        };
        
        var review = await reviewService.Add(menuItemId, User.GetId(), reviewToAdd);

        return Created($"{RoutesV1.MenuItems}/{menuItemId}/reviews/{review.Id}", review.Adapt<ReviewResponse>());
    }
    
    [HttpPatch("{menuItemId:long}/reviews/{reviewId:long}")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateReview(
        [FromRoute] long menuItemId,
        [FromRoute] long reviewId, 
        UpdateReviewRequest request)
    {
        if (!await IsReviewOfCustomer(reviewId))
            return Forbid();
        if (!await IsReviewOfMenuItem(reviewId, menuItemId))
            return NotFound();
        
        var review = new Review
        {
            Id = reviewId,
            Rating = request.Rating,
            Content = request.Content
        };
        
        await reviewService.UpdateReview(reviewId, review);

        return NoContent();
    }
    
    [HttpDelete("{menuItemId:long}/reviews/{reviewId:long}")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteReview(long menuItemId, long reviewId)
    {
        if (!await IsReviewOfCustomer(reviewId))
            return Forbid();
        if (!await IsReviewOfMenuItem(reviewId, menuItemId))
            return NotFound();
        
        await reviewService.Delete(reviewId);

        return NoContent();
    }

    private async Task<bool> IsReviewOfMenuItem(long reviewId, long menuItemId)
    {
        var review = await reviewService.GetById(reviewId);
        
        return review.MenuItem.Id == menuItemId;
    }

    private async Task<bool> IsReviewOfCustomer(long reviewId)
    {
        var review = await reviewService.GetById(reviewId);
        
        return review.Customer.Id == User.GetId();
    }

    private async Task<bool> IsMenuItemInCafe(long menuItemId)
    {
        var cafe = await cafeService.GetByAdminId(User.GetId());
        
        return cafe.MenuItems.Any(m => m.Id == menuItemId);
    }
}