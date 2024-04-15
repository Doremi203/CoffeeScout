using CoffeeScoutBackend.Api.Requests.V1.Beverages;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Api.Responses.V1.Beverages;
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
    [ProducesResponseType<AddBeverageTypeResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddBeverageType(AddBeverageTypeRequest request)
    {
        var addedBeverageType = 
            await beverageTypeService.Add(request.Adapt<BeverageType>());

        return Created($"{RoutesV1.BeverageTypes}/{addedBeverageType.Id}", addedBeverageType.Adapt<AddBeverageTypeResponse>());
    }
    
    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.CafeAdmin)},{nameof(Roles.Customer)}")]
    [ProducesResponseType<IReadOnlyCollection<GetBeverageTypeResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBeverageTypes([FromQuery] GetBeverageTypesRequest request)
    {
        var beverageTypes = await beverageTypeService.GetPage(request.PageSize, request.PageNumber);

        return Ok(beverageTypes.Adapt<IReadOnlyCollection<GetBeverageTypeResponse>>());
    }

    [HttpPatch("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBeverageType(
        [FromRoute] long id,
        UpdateBeverageTypeRequest request)
    {
        await beverageTypeService.Update(id, request.Adapt<BeverageType>());

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBeverageType([FromRoute] long id)
    {
        await beverageTypeService.Delete(id);

        return NoContent();
    }
}