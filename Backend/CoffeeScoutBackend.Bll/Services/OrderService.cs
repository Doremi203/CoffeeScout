using CoffeeScoutBackend.Bll.Infrastructure;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class OrderService(
    ICustomerService customerService,
    IMenuItemService menuItemService,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider
) : IOrderService
{
    public async Task<long> CreateOrderAsync(CreateOrderData orderData)
    {
        var customer = await customerService.GetByUserIdAsync(orderData.CustomerId);

        var order = new Order
        {
            Customer = customer,
            OrderDate = dateTimeProvider.UtcNow,
            OrderItems = await GetOrderItems(orderData.MenuItems),
            Status = OrderStatus.Pending
        };
        
        return await orderRepository.AddAsync(order);
    }

    private async Task<List<OrderItem>> GetOrderItems(IReadOnlyCollection<CreateOrderData.MenuItemData> orderDataMenuItems)
    {
        var orderItems = new List<OrderItem>();

        foreach (var orderDataMenuItem in orderDataMenuItems)
        {
            var menuItem = await menuItemService.GetByIdAsync(orderDataMenuItem.Id);
            var orderItem = new OrderItem
            {
                MenuItem = menuItem,
                Quantity = orderDataMenuItem.Quantity,
                PricePerItem = menuItem.Price,
                IsCompleted = false
            };

            orderItems.Add(orderItem);
        }

        return orderItems;
    }
}