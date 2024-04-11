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

        var (cafeId, orderItems) = await FormOrderItems(orderData.MenuItems);
        var cafe = await cafeService.GetById(cafeId);
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

    public async Task CompleteOrder(string adminId, long id)
    {
        var order = await GetById(id);
        AssertOrderStatus(order, OrderStatus.InProgress);
        var cafe = await cafeService.GetByAdminId(adminId);

        if (order.Cafe.Id != cafe.Id)
            throw new OrderCafeMismatchException(
                $"Order with id: {id} does not belong to cafe with id: {cafe.Id}");

        await orderRepository.UpdateStatus(id, OrderStatus.Completed);
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

    private async Task<(long cafeId, List<OrderItem> orderItems)> FormOrderItems(
        IReadOnlyCollection<CreateOrderData.MenuItemData> orderDataMenuItems)
    {
        var orderItems = new List<OrderItem>();
        var cafes = new HashSet<long>();

        foreach (var orderDataMenuItem in orderDataMenuItems)
        {
            var menuItem = await menuItemService.GetById(orderDataMenuItem.Id);
            cafes.Add(menuItem.Cafe.Id);

            var orderItem = new OrderItem
            {
                MenuItem = menuItem,
                Quantity = orderDataMenuItem.Quantity,
                PricePerItem = menuItem.Price,
            };

            orderItems.Add(orderItem);
        }

        if (cafes.Count != 1)
            throw new InvalidOrderDataException(
                "Order items must belong to the same cafe");

        return (cafes.Single(), orderItems);
    }
}