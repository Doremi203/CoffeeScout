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
    public async Task<IActionResult> AddBeverageTypeAsync(string name)
    {
        var newBeverageType = new BeverageType
        {
            Name = name
        };
        await beverageTypeService.AddBeverageTypeAsync(newBeverageType);

        return Created();
    }
    
    [HttpPatch("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBeverageTypeAsync(
        [FromRoute] long id,
        UpdateBeverageTypeRequest request)
    {
        await beverageTypeService.UpdateBeverageTypeNameAsync(id, request.Name);

        return NoContent();
    }
    
    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBeverageTypeAsync([FromRoute] long id)
    {
        await beverageTypeService.DeleteBeverageTypeAsync(id);

        return NoContent();
    }
    
}