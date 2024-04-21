using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using CoffeeScoutBackend.Api.Responses.V1.MenuItems;
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
    [ProducesResponseType<GetMenuItemResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMenuItemsByBeverageTypeInArea(
        [FromQuery] GetMenuItemsByBeverageTypeInAreaRequest request
    )
    {
        var menuItems = await menuItemService.GetAllInAreaByBeverageType(
            request.Location,
            request.RadiusInMeters,
            request.BeverageTypeId);

        return Ok(menuItems.Adapt<GetMenuItemResponse[]>());
    }

    [HttpGet("search")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<GetMenuItemResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchMenuItems(
        [FromQuery] SearchMenuItemsRequest request
    )
    {
        var menuItems =
            await menuItemService.Search(request.Name, request.Limit);

        return Ok(menuItems.Adapt<GetMenuItemResponse[]>());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<AddMenuItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddMenuItem([FromBody] AddMenuItemRequest request)
    {
        var menuItem = await menuItemService.Add(
            new AddMenuItemModel
            {
                CafeAdminId = User.GetId(),
                Name = request.Name,
                Price = request.Price,
                SizeInMl = request.SizeInMl,
                BeverageTypeId = request.BeverageTypeId
            });

        return Created($"{RoutesV1.MenuItems}/{menuItem.Id}", menuItem.Adapt<AddMenuItemResponse>());
    }

    [HttpPatch("{id:long}")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMenuItem(
        [FromRoute] long id,
        [FromBody] UpdateMenuItemRequest request)
    {
        if (!await IsMenuItemInCafe(id))
            return Forbid();

        await menuItemService.Update(new UpdateMenuItemModel
        {
            Id = id,
            Name = request.Name,
            Price = request.Price,
            SizeInMl = request.SizeInMl,
            BeverageTypeId = request.BeverageTypeId
        });

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMenuItem([FromRoute] long id)
    {
        if (!await IsMenuItemInCafe(id))
            return Forbid();

        await menuItemService.Delete(id);

        return NoContent();
    }

    [HttpPost("{menuItemId:long}/reviews")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<AddReviewResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddReview(
        [FromRoute] long menuItemId, 
        [FromBody] AddReviewRequest request)
    {
        var reviewToAdd = new Review
        {
            Rating = request.Rating,
            Content = request.Content
        };

        var review = await reviewService.Add(menuItemId, User.GetId(), reviewToAdd);

        return Created($"{RoutesV1.MenuItems}/{menuItemId}/reviews/{review.Id}", review.Adapt<AddReviewResponse>());
    }

    [HttpGet("{menuItemId:long}/reviews")]
    [Authorize(Roles = $"{nameof(Roles.Customer)},{nameof(Roles.CafeAdmin)}")]
    [ProducesResponseType<GetReviewResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReviews([FromRoute] long menuItemId)
    {
        var reviews = await reviewService.GetByMenuItemId(menuItemId);

        return Ok(reviews.Adapt<GetReviewResponse[]>());
    }

    private async Task<bool> IsMenuItemInCafe(long menuItemId)
    {
        var cafe = await cafeService.GetByAdminId(User.GetId());

        return cafe.MenuItems.Any(m => m.Id == menuItemId);
    }
}