using CoffeeScoutBackend.Api.Requests.V1.CoffeeChains;
using CoffeeScoutBackend.Api.Responses.V1.CoffeeChains;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route($"api/{RoutesV1.CoffeeChains}")]
public class CoffeeChainsController(
    ICoffeeChainService coffeeChainService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.CafeAdmin)},{nameof(Roles.Customer)}")]
    [ProducesResponseType<GetCoffeeChainResponse[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCoffeeChains(
        [FromQuery] GetCoffeeChainsRequest request
    )
    {
        var coffeeChains =
            await coffeeChainService.GetPage(
                request.Pagination.PageSize,
                request.Pagination.PageNumber);

        return Ok(coffeeChains.Adapt<GetCoffeeChainResponse[]>());
    }

    [HttpGet("{id:long}")]
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.CafeAdmin)},{nameof(Roles.Customer)}")]
    [ProducesResponseType<GetCoffeeChainResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCoffeeChainById(long id)
    {
        var coffeeChain = await coffeeChainService.GetById(id);

        return Ok(coffeeChain.Adapt<GetCoffeeChainResponse>());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType<AddCoffeeChainResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCoffeeChain(
        [FromBody] AddCoffeeChainRequest request
    )
    {
        var coffeeChain = await coffeeChainService.Add(request.Adapt<CoffeeChain>());

        return Created(
            $"/api/{RoutesV1.CoffeeChains}/{coffeeChain.Id}",
            coffeeChain.Adapt<AddCoffeeChainResponse>()
        );
    }

    [HttpPatch("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCoffeeChain(
        long id,
        [FromBody] UpdateCoffeeChainRequest request)
    {
        await coffeeChainService.Update(id, request.Adapt<CoffeeChain>());

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCoffeeChain(long id)
    {
        await coffeeChainService.Delete(id);

        return NoContent();
    }
}