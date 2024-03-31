using CoffeeScoutBackend.Bll.Infrastructure;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class OrderService(
    ICustomerService customerService,
    IMenuItemService menuItemService,
    ICafeService cafeService,
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
            OrderItems = await FormOrderItems(orderData.MenuItems),
            Status = OrderStatus.Pending
        };

        return await orderRepository.AddAsync(order);
    }

    public async Task<IReadOnlyCollection<Order>> GetCafeOrdersAsync(
        string currentCafeAdminId,
        OrderStatus status,
        DateTime from
    )
    {
        var cafe = await cafeService.GetByAdminIdAsync(currentCafeAdminId);
        var orders = await orderRepository.GetOrdersAsync(status, from);

        foreach (var order in orders)
        {
            order.OrderItems = order.OrderItems
                .Where(oi => oi.MenuItem.Cafe.Id == cafe.Id)
                .ToList();
        }
        
        return orders;
    }

    private async Task<List<OrderItem>> FormOrderItems(
        IReadOnlyCollection<CreateOrderData.MenuItemData> orderDataMenuItems)
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