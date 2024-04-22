using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using CoffeeScoutBackend.Api.Requests.V1.Orders;
using CoffeeScoutBackend.Api.Responses.V1.Cafes;
using CoffeeScoutBackend.Api.Responses.V1.Orders;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.Domain.ServiceModels;
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
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCafes([FromQuery] GetCafesRequest request)
    {
        var cafes =
            await cafeService.GetCafesInArea(
                request.Location,
                request.RadiusInMeters);

        return Ok(cafes.Adapt<GetCafeResponse[]>());
    }

    [HttpGet("info")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<GetCafeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCafeForCafeAdmin()
    {
        var cafe = await cafeService.GetByAdminId(User.GetId());

        return Ok(cafe.Adapt<GetCafeResponse>());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType<AddCafeResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCafe([FromBody] AddCafeRequest request)
    {
        var cafe = new Cafe
        {
            Name = request.Name,
            Location = request.Location,
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCafe([FromRoute] long id)
    {
        var cafe = await cafeService.GetById(id);

        return Ok(cafe.Adapt<GetCafeResponse>());
    }

    [HttpPatch]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCafe([FromBody] UpdateCafeRequest request)
    {
        var cafe = new Cafe
        {
            Name = request.Name,
            Location = request.Location,
            Address = request.Address,
            WorkingHours = request.WorkingHours.Adapt<IReadOnlyCollection<WorkingHours>>()
        };

        await cafeService.UpdateCafe(User.GetId(), cafe);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCafe([FromRoute] long id)
    {
        await cafeService.DeleteCafe(id);

        return NoContent();
    }

    [HttpPost("{id:long}/orders")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<GetOrderResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlaceOrder(
        [FromRoute] long id,
        [FromBody] PlaceOrderRequest request)
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
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCafeOrders(
        [FromQuery] GetOrdersRequest request)
    {
        var orders =
            await orderService.GetCafeOrders(
                User.GetId(),
                new GetOrdersModel
                {
                    Status = request.Status,
                    PageSize = request.Pagination.PageSize,
                    PageNumber = request.Pagination.PageNumber
                });

        return Ok(orders.Adapt<GetOrderResponse[]>());
    }

    [HttpPatch("orders/{id:long}/complete")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteCafeOrderPart([FromRoute] long id)
    {
        await orderService.CompleteOrder(User.GetId(), id);

        return NoContent();
    }

    [HttpPatch("orders/{id:long}/cancel")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelOrder([FromRoute] long id)
    {
        await orderService.CafeCancelOrder(User.GetId(), id);

        return NoContent();
    }

    [HttpGet("{id:long}/menuItems")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<GetCafeMenuItemResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCafeMenuItems([FromRoute] long id)
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