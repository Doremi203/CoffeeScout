using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using CoffeeScoutBackend.Api.Requests.V1.Customers;
using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using CoffeeScoutBackend.Api.Responses.V1.Beverages;
using CoffeeScoutBackend.Api.Responses.V1.Customers;
using CoffeeScoutBackend.Api.Responses.V1.MenuItems;
using CoffeeScoutBackend.Api.Responses.V1.Orders;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.Customers)]
[Authorize(Roles = nameof(Roles.Customer))]
public class CustomersController(
    ICustomerService customerService,
    IOrderService orderService,
    IReviewService reviewService
) : ControllerBase
{
    [HttpGet("info")]
    [ProducesResponseType<GetCustomerInfoResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerInfo()
    {
        var customer = await customerService.GetInfo(User.GetId());

        return Ok(customer.Adapt<GetCustomerInfoResponse>());
    }

    [HttpPatch("info")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateInfo(UpdateCustomerInfoRequest request)
    {
        await customerService.UpdateInfo(User.GetId(), request.Adapt<CustomerInfo>());

        return NoContent();
    }

    [HttpPost("favored-menu-items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddFavoredMenuItem([FromQuery] long menuItemId)
    {
        await customerService.AddFavoredMenuItem(User.GetId(), menuItemId);

        return Created($"{RoutesV1.Customers}/favored-menu-items/{menuItemId}", null);
    }

    [HttpGet("favored-menu-items")]
    [ProducesResponseType<GetMenuItemResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFavoredMenuItems()
    {
        var favoredMenuItems =
            await customerService.GetFavoredMenuItems(User.GetId());

        return Ok(favoredMenuItems.Adapt<GetMenuItemResponse[]>());
    }

    [HttpDelete("favored-menu-items/{menuItemId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveFavoredMenuItem(long menuItemId)
    {
        await customerService.RemoveFavoredMenuItem(User.GetId(), menuItemId);

        return NoContent();
    }

    [HttpGet("favored-beverage-types")]
    [ProducesResponseType<GetBeverageTypeResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFavoredBeverageTypes()
    {
        var favoredBeverageTypes =
            await customerService.GetFavoredBeverageTypes(User.GetId());

        return Ok(favoredBeverageTypes.Adapt<GetBeverageTypeResponse[]>());
    }

    [HttpGet("orders")]
    [ProducesResponseType<GetOrderResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders(
        [FromQuery] GetOrdersRequest request)
    {
        var orders =
            await orderService.GetCustomerOrders(
                User.GetId(),
                new GetOrdersModel
                {
                    Status = request.Status,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber
                });

        return Ok(orders.Adapt<GetOrderResponse[]>());
    }

    [HttpPatch("orders/{id:long}/pay")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PayOrder(long id)
    {
        await orderService.PayOrder(User.GetId(), id);

        return NoContent();
    }

    [HttpPatch("orders/{id:long}/cancel")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CancelOrder(long id)
    {
        await orderService.CustomerCancelOrder(User.GetId(), id);

        return NoContent();
    }

    [HttpPatch("reviews/{reviewId:long}")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateReview(
        [FromRoute] long reviewId,
        UpdateReviewRequest request)
    {
        if (!await IsReviewOfCustomer(reviewId))
            return Forbid();

        await reviewService.UpdateReview(reviewId, request.Adapt<Review>());

        return NoContent();
    }

    [HttpDelete("reviews/{reviewId:long}")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteReview([FromRoute] long reviewId)
    {
        if (!await IsReviewOfCustomer(reviewId))
            return Forbid();

        await reviewService.Delete(reviewId);

        return NoContent();
    }

    private async Task<bool> IsReviewOfCustomer(long reviewId)
    {
        var review = await reviewService.GetById(reviewId);

        return review.Customer.Id == User.GetId();
    }
}