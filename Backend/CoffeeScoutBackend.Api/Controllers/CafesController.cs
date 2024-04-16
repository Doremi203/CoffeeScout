using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using CoffeeScoutBackend.Api.Requests.V1.Orders;
using CoffeeScoutBackend.Api.Responses.V1.Cafes;
using CoffeeScoutBackend.Api.Responses.V1.Orders;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.Cafes)]
public class CafesController(
    ICafeService cafeService,
    IOrderService orderService,
    IMenuItemService menuItemService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<GetCafeResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafes([FromQuery] GetCafesRequest request)
    {
        var cafes =
            await cafeService.GetCafesInArea(
                new Location
                {
                    Longitude = request.Longitude,
                    Latitude = request.Latitude
                },
                request.RadiusInMeters);

        return Ok(cafes.Adapt<GetCafeResponse[]>());
    }

    [HttpGet("info")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<GetCafeResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafeForCafeAdmin()
    {
        var cafe = await cafeService.GetByAdminId(User.GetId());

        return Ok(cafe.Adapt<GetCafeResponse>());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType<AddCafeResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCafe(AddCafeRequest request)
    {
        var cafe = new Cafe
        {
            Name = request.Name,
            Location = new Location
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            Address = request.Address,
            CoffeeChain = new CoffeeChain
            {
                Id = request.CoffeeChainId
            },
            WorkingHours = request.WorkingHours.Adapt<IReadOnlyCollection<WorkingHours>>()
        };

        var newCafe = await cafeService.AddCafe(cafe);

        return Created($"{RoutesV1.Cafes}/{newCafe.Id}", newCafe.Adapt<AddCafeResponse>());
    }

    [HttpGet("{id:long}")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<GetCafeResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafe([FromRoute] long id)
    {
        var cafe = await cafeService.GetById(id);

        return Ok(cafe.Adapt<GetCafeResponse>());
    }

    [HttpPatch]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateCafe(UpdateCafeRequest request)
    {
        var cafe = new Cafe
        {
            Name = request.Name,
            Location = new Location
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            Address = request.Address,
            WorkingHours = request.WorkingHours.Adapt<IReadOnlyCollection<WorkingHours>>()
        };

        await cafeService.UpdateCafe(User.GetId(), cafe);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCafe([FromRoute] long id)
    {
        await cafeService.DeleteCafe(id);

        return NoContent();
    }

    [HttpPost("{id:long}/orders")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<GetOrderResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PlaceOrder(
        [FromRoute] long id,
        PlaceOrderRequest request)
    {
        var orderData = new CreateOrderData
        {
            CustomerId = User.GetId(),
            CafeId = id,
            MenuItems = request.MenuItems
                .Adapt<IReadOnlyCollection<CreateOrderData.MenuItemData>>()
        };
        var order = await orderService.CreateOrder(orderData);

        return Created($"{RoutesV1.Orders}/{order.Id}", order.Adapt<GetOrderResponse>());
    }

    [HttpGet("orders")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<GetOrderResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafeOrders(
        [FromQuery] GetOrdersRequest request)
    {
        var orders =
            await orderService.GetCafeOrders(
                User.GetId(),
                new GetOrdersModel
                {
                    Status = request.Status,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber
                });

        return Ok(orders.Adapt<GetOrderResponse[]>());
    }

    [HttpPatch("orders/{id:long}/complete")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CompleteCafeOrderPart(long id)
    {
        await orderService.CompleteOrder(User.GetId(), id);

        return NoContent();
    }

    [HttpPatch("orders/{id:long}/cancel")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CancelOrder(long id)
    {
        await orderService.CafeCancelOrder(User.GetId(), id);

        return NoContent();
    }

    [HttpGet("{id:long}/menuItems")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<GetCafeMenuItemResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafeMenuItems(long id)
    {
        var menuItems = await menuItemService.GetByCafeId(id);

        return Ok(menuItems.Adapt<GetCafeMenuItemResponse[]>());
    }

    [HttpGet("menuItems")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<GetCafeMenuItemResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafeMenuItemsForCafeAdmin()
    {
        var menuItems = await menuItemService.GetByCafeAdmin(User.GetId());

        return Ok(menuItems.Adapt<GetCafeMenuItemResponse[]>());
    }
}