using CoffeeScoutBackend.Api.Requests.V1.Orders;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Orders;

public class PlaceOrderRequestValidator : AbstractValidator<PlaceOrderRequest>
{
    public PlaceOrderRequestValidator(IValidator<OrderMenuItemRequest> orderMenuItemRequestValidator)
    {
        RuleForEach(x => x.MenuItems)
            .SetValidator(orderMenuItemRequestValidator);
    }
}