using System.Transactions;
using CoffeeScoutBackend.Bll.Infrastructure;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class OrderService(
    ICustomerService customerService,
    IMenuItemService menuItemService,
    ICafeService cafeService,
    IPaymentService paymentService,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider
) : IOrderService
{
    public async Task<Order> CreateOrder(CreateOrderData orderData)
    {
        var customer = await customerService.GetByUserId(orderData.CustomerId);

        var order = new Order
        {
            Customer = customer,
            Date = dateTimeProvider.UtcNow,
            OrderItems = await FormOrderItems(orderData.MenuItems),
            Status = OrderStatus.Pending
        };

        return await orderRepository.Add(order);
    }

    public async Task<Order> GetById(long id)
    {
        var order = await orderRepository.GetById(id)
                    ?? throw new OrderNotFoundException(
                        $"Order with id: {id} not found", id);

        return order;
    }

    public async Task<IReadOnlyCollection<Order>> GetCafeOrders(
        string currentCafeAdminId,
        OrderStatus status,
        DateTime from
    )
    {
        var cafe = await cafeService.GetByAdminId(currentCafeAdminId);
        var orders = await orderRepository.GetByCafeId(cafe.Id, status, from);

        FilterOutUnrelatedOrderItems();

        return orders;

        void FilterOutUnrelatedOrderItems()
        {
            foreach (var order in orders)
            {
                order.OrderItems = order.OrderItems
                    .Where(oi => oi.MenuItem.Cafe.Id == cafe.Id)
                    .ToList();
            }
        }
    }

    public async Task<IReadOnlyCollection<Order>> GetCustomerOrders(
        string userId,
        OrderStatus status,
        DateTime from
    )
    {
        return await orderRepository.GetByUserId(userId, status, from);
    }

    public async Task CompleteCafeOrderPart(string adminId, long id)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var order = await GetById(id);
        AssertOrderStatus(order, OrderStatus.InProgress);

        var cafe = await cafeService.GetByAdminId(adminId);

        foreach (var orderItem in order.OrderItems
                     .Where(orderItem => orderItem.MenuItem.Cafe.Id == cafe.Id))
        {
            await orderRepository.UpdateOrderItemCompletionStatus(
                orderItem.Order.Id, 
                orderItem.MenuItem.Id,
                true);
        }
        
        transaction.Complete();
    }

    public async Task CancelOrder(long id)
    {
        var order = await GetById(id);
        AssertOrderNotCompleted(id, order);
        
        await orderRepository.UpdateStatus(id, OrderStatus.Cancelled);
    }

    public async Task PayOrder(string getId, long id)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var order = await GetById(id);
        AssertOrderStatus(order, OrderStatus.Pending);
        
        await paymentService.ProcessPayment(getId, GetTotalAmount(order));

        await orderRepository.UpdateStatus(id, OrderStatus.InProgress);
        
        transaction.Complete();
    }

    private static decimal GetTotalAmount(Order order)
    {
        return order.OrderItems.Sum(oi => oi.Quantity * oi.PricePerItem);
    }

    private static void AssertOrderNotCompleted(long id, Order order)
    {
        if (order.Status == OrderStatus.Completed)
            throw new InvalidOrderStatusException(
                $"Order with id: {id} is already completed",
                OrderStatus.Completed,
                id);
    }

    private static void AssertOrderStatus(Order order, OrderStatus expectedStatus)
    {
        if (order.Status != expectedStatus)
            throw new InvalidOrderStatusException(
                $"Order with id: {order.Id} is not in {expectedStatus} status",
                expectedStatus,
                order.Id);
    }

    private async Task<List<OrderItem>> FormOrderItems(
        IReadOnlyCollection<CreateOrderData.MenuItemData> orderDataMenuItems)
    {
        var orderItems = new List<OrderItem>();

        foreach (var orderDataMenuItem in orderDataMenuItems)
        {
            var menuItem = await menuItemService.GetById(orderDataMenuItem.Id);
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