using CoffeeScoutBackend.Api.Requests.V1.Orders;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Orders;

public class OrderMenuItemRequestValidator : AbstractValidator<OrderMenuItemRequest>
{
    public OrderMenuItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}