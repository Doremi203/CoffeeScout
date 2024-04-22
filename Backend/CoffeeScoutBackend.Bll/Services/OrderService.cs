using System.Transactions;
using CoffeeScoutBackend.Bll.Infrastructure;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
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
        var cafe = await cafeService.GetById(orderData.CafeId);
        
        var orderItems = await FormOrderItems(
            orderData.MenuItems, orderData.CafeId);
        
        var order = new Order
        {
            Customer = customer,
            Date = dateTimeProvider.UtcNow,
            Cafe = cafe,
            OrderItems = orderItems,
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

    public async Task<IReadOnlyCollection<Order>> GetCafeOrders(string adminId, GetOrdersModel model)
    {
        var cafe = await cafeService.GetByAdminId(adminId);
        
        return await orderRepository.GetByCafeId(cafe.Id, model);
    }

    public async Task<IReadOnlyCollection<Order>> GetCustomerOrders(
        string userId,
        GetOrdersModel model
    )
    {
        return await orderRepository.GetByUserId(userId, model);
    }

    public async Task CompleteOrder(string adminId, long id)
    {
        var order = await GetById(id);
        AssertOrderStatus(order, OrderStatus.InProgress);
        var cafe = await cafeService.GetByAdminId(adminId);

        if (order.Cafe.Id != cafe.Id)
            throw new OrderNotFoundException(
                $"Order with id: {id} not found in cafe with id: {cafe.Id}",
                id);

        await orderRepository.UpdateStatus(id, OrderStatus.Completed);
    }

    public async Task CafeCancelOrder(string adminId, long id)
    {
        var order = await GetById(id);
        AssertOrderNotCompleted(id, order);

        var cafe = await cafeService.GetByAdminId(adminId);

        if (order.Cafe.Id != cafe.Id)
            throw new OrderNotFoundException(
                $"Order with id: {id} not found in cafe with id: {cafe.Id}",
                id);

        await orderRepository.UpdateStatus(id, OrderStatus.Cancelled);
    }
    
    public async Task CustomerCancelOrder(string userId, long id)
    {
        var order = await GetById(id);
        AssertOrderNotCompleted(id, order);

        if (order.Customer.Id != userId)
            throw new OrderNotFoundException(
                $"Order with id: {id} not found for user with id: {userId}",
                id);

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
        IReadOnlyCollection<CreateOrderData.MenuItemData> orderDataMenuItems,
        long cafeId)
    {
        var orderItems = new List<OrderItem>();

        foreach (var orderDataMenuItem in orderDataMenuItems)
        {
            var menuItem = await menuItemService.GetById(orderDataMenuItem.Id);
            if (menuItem.Cafe.Id != cafeId)
                throw new InvalidOrderDataException(
                    "Order items must belong to the same cafe");

            var orderItem = new OrderItem
            {
                MenuItem = menuItem,
                Quantity = orderDataMenuItem.Quantity,
                PricePerItem = menuItem.Price,
            };

            orderItems.Add(orderItem);
        }

        return orderItems;
    }
}