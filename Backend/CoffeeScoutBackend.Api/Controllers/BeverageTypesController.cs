using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/beverage-types")]
public class BeverageTypesController(
    IBeverageTypeService beverageTypeService
) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddBeverageType(string name)
    {
        var newBeverageType = new BeverageType
        {
            Name = name
        };
        await beverageTypeService.AddBeverageType(newBeverageType);

        return Created();
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