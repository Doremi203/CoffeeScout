using CoffeeScoutBackend.Api.Requests.V1.Beverages;
using CoffeeScoutBackend.Api.Responses.V1.Beverages;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GetBeverageTypeResponse = CoffeeScoutBackend.Api.Responses.V1.Beverages.GetBeverageTypeResponse;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.BeverageTypes)]
public class BeverageTypesController(
    IBeverageTypeService beverageTypeService
) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType<AddBeverageTypeResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBeverageType([FromBody] AddBeverageTypeRequest request)
    {
        var addedBeverageType =
            await beverageTypeService.Add(request.Adapt<BeverageType>());

        return Created($"{RoutesV1.BeverageTypes}/{addedBeverageType.Id}",
            addedBeverageType.Adapt<AddBeverageTypeResponse>());
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.CafeAdmin)},{nameof(Roles.Customer)}")]
    [ProducesResponseType<GetBeverageTypeResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBeverageTypes([FromQuery] GetBeverageTypesRequest request)
    {
        var beverageTypes =
            await beverageTypeService.GetPage(request.Pagination.PageSize, request.Pagination.PageNumber);

        return Ok(beverageTypes.Adapt<GetBeverageTypeResponse[]>());
    }

    [HttpPatch("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBeverageType(
        [FromRoute] long id,
        [FromBody] UpdateBeverageTypeRequest request)
    {
        await beverageTypeService.Update(id, request.Adapt<BeverageType>());

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBeverageType([FromRoute] long id)
    {
        await beverageTypeService.Delete(id);

        return NoContent();
    }
}