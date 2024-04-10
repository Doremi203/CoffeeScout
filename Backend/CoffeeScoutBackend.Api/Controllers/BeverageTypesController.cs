using CoffeeScoutBackend.Api.Requests.V1.Beverages;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.BeverageTypes)]
public class BeverageTypesController(
    IBeverageTypeService beverageTypeService
) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType<BeverageTypeResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddBeverageType(AddBeverageTypeRequest request)
    {
        var newBeverageType = new BeverageType
        {
            Name = request.Name
        };
        var beverageType = await beverageTypeService.AddBeverageType(newBeverageType);

        return Created($"{RoutesV1.BeverageTypes}/{beverageType.Id}", beverageType.Adapt<BeverageTypeResponse>());
    }
    
    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.CafeAdmin)},{nameof(Roles.Customer)}")]
    [ProducesResponseType<IReadOnlyCollection<BeverageTypeResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBeverageTypes([FromQuery] GetBeverageTypesRequest request)
    {
        var beverageTypes = await beverageTypeService.GetBeverageTypes(request.PageSize, request.PageNumber);

        return Ok(beverageTypes.Adapt<IReadOnlyCollection<BeverageTypeResponse>>());
    }

    [HttpPatch("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBeverageType(
        [FromRoute] long id,
        UpdateBeverageTypeRequest request)
    {
        await beverageTypeService.UpdateBeverageTypeName(id, request.Name);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBeverageType([FromRoute] long id)
    {
        await beverageTypeService.DeleteBeverageType(id);

        return NoContent();
    }
}