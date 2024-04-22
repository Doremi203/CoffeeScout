using CoffeeScoutBackend.Bll.Infrastructure;
using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.Domain.ServiceModels;
using CoffeeScoutBackend.UnitTests.Fakers;
using FluentAssertions;
using Mapster;
using Moq;

namespace CoffeeScoutBackend.UnitTests.Tests;

public class OrderServiceTests
{
    private readonly Mock<ICafeService> _cafeServiceMock = new(MockBehavior.Strict);
    private readonly Mock<ICustomerService> _customerServiceMock = new(MockBehavior.Strict);
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new(MockBehavior.Strict);
    private readonly Mock<IMenuItemService> _menuItemServiceMock = new(MockBehavior.Strict);
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new(MockBehavior.Strict);
    private readonly IOrderService _orderService;
    private readonly Mock<IPaymentService> _paymentServiceMock = new(MockBehavior.Strict);

    public OrderServiceTests()
    {
        _orderService = new OrderService(
            _customerServiceMock.Object,
            _menuItemServiceMock.Object,
            _cafeServiceMock.Object,
            _paymentServiceMock.Object,
            _orderRepositoryMock.Object,
            _dateTimeProviderMock.Object
        );
    }

    [Fact]
    public async Task CreateOrder_WithValidData_ShouldReturnCreatedOrder()
    {
        // Arrange
        var dateTime = DateTime.UtcNow;
        var orderData = CreateOrderDataFaker.Generate();
        var customer = CustomerFaker.Generate()[0]
            .WithId(orderData.CustomerId);
        var menuItems = MenuItemFaker.Generate(5);
        var cafe = CafeFaker.Generate()[0]
            .WithId(orderData.CafeId)
            .WithMenuItems(menuItems);
        orderData = orderData.WithMenuItems(menuItems.Adapt<CreateOrderData.MenuItemData[]>());

        var expectedOrder = new Order
        {
            Customer = customer,
            Date = dateTime,
            Cafe = cafe,
            OrderItems = orderData.MenuItems.Adapt<List<OrderItem>>(),
            Status = OrderStatus.Pending
        };

        _customerServiceMock
            .Setup(x => x.GetByUserId(customer.Id))
            .ReturnsAsync(customer);
        _cafeServiceMock
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(cafe);
        for (var i = 0; i < menuItems.Length; i++)
        {
            menuItems[i] = menuItems[i].WithCafe(cafe);
            var menuItem = menuItems[i];
            _menuItemServiceMock
                .Setup(x => x.GetById(menuItem.Id))
                .ReturnsAsync(menuItem);
        }

        _dateTimeProviderMock
            .Setup(x => x.UtcNow)
            .Returns(dateTime);
        _orderRepositoryMock
            .Setup(x => x.Add(It.IsAny<Order>()))
            .ReturnsAsync(expectedOrder);

        // Act
        var result = await _orderService.CreateOrder(orderData);

        // Assert
        result.Should().BeEquivalentTo(expectedOrder);

        _customerServiceMock.Verify(x => x.GetByUserId(customer.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetById(cafe.Id), Times.Once);
        foreach (var menuItem in menuItems) _menuItemServiceMock.Verify(x => x.GetById(menuItem.Id), Times.Once);

        _dateTimeProviderMock.Verify(x => x.UtcNow, Times.Once);
        _orderRepositoryMock.Verify(x => x.Add(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var orderData = CreateOrderDataFaker.Generate();
        var expectedException = new CustomerNotFoundException(
            $"Customer with id: {orderData.CustomerId} not found",
            orderData.CustomerId);

        _customerServiceMock
            .Setup(x => x.GetByUserId(orderData.CustomerId))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _orderService.CreateOrder(orderData);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage(expectedException.Message);

        _customerServiceMock.Verify(x => x.GetByUserId(orderData.CustomerId), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_WithNonExistingCafe_ShouldThrowCafeNotFoundException()
    {
        // Arrange
        var orderData = CreateOrderDataFaker.Generate();
        var customer = CustomerFaker.Generate()[0]
            .WithId(orderData.CustomerId);
        var expectedException = new CafeNotFoundException(
            $"Cafe with id: {orderData.CafeId} not found",
            orderData.CafeId);

        _customerServiceMock
            .Setup(x => x.GetByUserId(customer.Id))
            .ReturnsAsync(customer);
        _cafeServiceMock
            .Setup(x => x.GetById(orderData.CafeId))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _orderService.CreateOrder(orderData);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage(expectedException.Message);

        _customerServiceMock.Verify(x => x.GetByUserId(customer.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetById(orderData.CafeId), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_WithNonExistingMenuItem_ShouldThrowMenuItemNotFoundException()
    {
        // Arrange
        var orderData = CreateOrderDataFaker.Generate();
        var customer = CustomerFaker.Generate()[0]
            .WithId(orderData.CustomerId);
        var menuItems = MenuItemFaker.Generate(5);
        var cafe = CafeFaker.Generate()[0]
            .WithId(orderData.CafeId)
            .WithMenuItems(menuItems);
        orderData = orderData.WithMenuItems(menuItems.Adapt<CreateOrderData.MenuItemData[]>());
        var expectedException = new MenuItemNotFoundException(
            $"MenuItem with id: {menuItems[0].Id} not found",
            menuItems[0].Id);

        _customerServiceMock
            .Setup(x => x.GetByUserId(customer.Id))
            .ReturnsAsync(customer);
        _cafeServiceMock
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(cafe);
        for (var i = 0; i < menuItems.Length; i++)
        {
            menuItems[i] = menuItems[i].WithCafe(cafe);
            var menuItem = menuItems[i];
            _menuItemServiceMock
                .Setup(x => x.GetById(menuItem.Id))
                .ThrowsAsync(expectedException);
        }

        // Act
        Func<Task> act = async () => await _orderService.CreateOrder(orderData);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage(expectedException.Message);

        _customerServiceMock.Verify(x => x.GetByUserId(customer.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetById(cafe.Id), Times.Once);
        _menuItemServiceMock.Verify(x => x.GetById(menuItems[0].Id), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_WithItemsFromDifferentCafes_ShouldThrowInvalidOrderDataException()
    {
        // Arrange
        var dateTime = DateTime.UtcNow;
        var orderData = CreateOrderDataFaker.Generate();
        var customer = CustomerFaker.Generate()[0]
            .WithId(orderData.CustomerId);
        var menuItems = MenuItemFaker.Generate(5);
        var cafe = CafeFaker.Generate()[0]
            .WithId(orderData.CafeId)
            .WithMenuItems(menuItems);
        var otherCafe = CafeFaker.Generate()[0]
            .WithId(orderData.CafeId + 1)
            .WithMenuItems(menuItems);
        orderData = orderData.WithMenuItems(menuItems.Adapt<CreateOrderData.MenuItemData[]>());
        var expectedException = new InvalidOrderDataException(
            "Order items must belong to the same cafe");

        _dateTimeProviderMock
            .Setup(x => x.UtcNow)
            .Returns(dateTime);
        _customerServiceMock
            .Setup(x => x.GetByUserId(customer.Id))
            .ReturnsAsync(customer);
        _cafeServiceMock
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(cafe);
        for (var i = 0; i < menuItems.Length; i++)
        {
            menuItems[i] = menuItems[i].WithCafe(cafe);
            if (i == 0) menuItems[i] = menuItems[i].WithCafe(otherCafe);

            var menuItem = menuItems[i];
            _menuItemServiceMock
                .Setup(x => x.GetById(menuItem.Id))
                .ReturnsAsync(menuItem);
        }

        // Act
        Func<Task> act = async () => await _orderService.CreateOrder(orderData);

        // Assert
        await act.Should().ThrowAsync<InvalidOrderDataException>()
            .WithMessage(expectedException.Message);

        _customerServiceMock.Verify(x => x.GetByUserId(customer.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetById(cafe.Id), Times.Once);
        _menuItemServiceMock.Verify(x => x.GetById(menuItems[0].Id), Times.Once);
    }

    [Fact]
    public async Task GetById_WithExistingOrder_ShouldReturnOrder()
    {
        // Arrange
        var order = OrderFaker.Generate();
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);

        // Act
        var result = await _orderService.GetById(order.Id);

        // Assert
        result.Should().BeEquivalentTo(order);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
    }

    [Fact]
    public async Task GetById_WithNonExistingOrder_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var orderId = 1;
        _orderRepositoryMock
            .Setup(x => x.GetById(orderId))
            .ReturnsAsync(default(Order));

        // Act
        Func<Task> act = async () => await _orderService.GetById(orderId);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage($"Order with id: {orderId} not found");

        _orderRepositoryMock.Verify(x => x.GetById(orderId), Times.Once);
    }

    [Fact]
    public async Task GetCafeOrders_WithExistingCafe_ShouldReturnOrders()
    {
        // Arrange
        const int pageSize = 5;
        const int pageNumber = 1;
        var cafeAdmin = CafeAdminFaker.Generate();
        var cafe = CafeFaker.Generate()[0]
            .WithAdmins(cafeAdmin)
            .WithId(1);
        var orders = Enumerable.Range(0, pageSize)
            .Select(_ =>
                OrderFaker.Generate()
                    .WithCafe(cafe)
                    .WithStatus(OrderStatus.Pending))
            .ToArray();
        _cafeServiceMock
            .Setup(x => x.GetByAdminId(cafeAdmin[0].Id))
            .ReturnsAsync(cafe);
        var model = new GetOrdersModel
        {
            Status = OrderStatus.Pending,
            PageSize = pageSize,
            PageNumber = pageNumber
        };
        _orderRepositoryMock
            .Setup(x => x.GetByCafeId(cafe.Id, model))
            .ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetCafeOrders(cafeAdmin[0].Id, model);

        // Assert
        result.Should().BeEquivalentTo(orders);

        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin[0].Id), Times.Once);
        _orderRepositoryMock.Verify(x => x.GetByCafeId(cafe.Id, model), Times.Once);
    }

    [Fact]
    public async Task GetCafeOrders_WithNonExistingCafe_ShouldThrowCafeNotFoundException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate();
        var expectedException = new CafeNotFoundException(
            $"Cafe with admin id: {cafeAdmin[0].Id} not found");
        _cafeServiceMock
            .Setup(x => x.GetByAdminId(cafeAdmin[0].Id))
            .ThrowsAsync(expectedException);
        var model = new GetOrdersModel
        {
            Status = OrderStatus.Pending,
            PageSize = 5,
            PageNumber = 1
        };

        // Act
        Func<Task> act = async () => await _orderService.GetCafeOrders(cafeAdmin[0].Id, model);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage(expectedException.Message);

        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin[0].Id), Times.Once);
    }

    [Fact]
    public async Task GetCustomerOrders_WithExistingCustomer_ShouldReturnOrders()
    {
        // Arrange
        const int pageSize = 5;
        const int pageNumber = 1;
        var customer = CustomerFaker.Generate()[0];
        var orders = Enumerable.Range(0, pageSize)
            .Select(_ =>
                OrderFaker.Generate()
                    .WithCustomer(customer)
                    .WithStatus(OrderStatus.Pending))
            .ToArray();
        var model = new GetOrdersModel
        {
            Status = OrderStatus.Pending,
            PageSize = pageSize,
            PageNumber = pageNumber
        };
        _orderRepositoryMock
            .Setup(x => x.GetByUserId(customer.Id, model))
            .ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetCustomerOrders(customer.Id, model);

        // Assert
        result.Should().BeEquivalentTo(orders);

        _orderRepositoryMock.Verify(x => x.GetByUserId(customer.Id, model), Times.Once);
    }

    [Fact]
    public async Task CompleteOrder_WithExistingOrderInProgress_ShouldCompleteOrder()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var cafe = CafeFaker.Generate()[0]
            .WithId(1);
        var order = OrderFaker.Generate()
            .WithCafe(cafe)
            .WithStatus(OrderStatus.InProgress);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);
        _cafeServiceMock
            .Setup(x => x.GetByAdminId(cafeAdmin.Id))
            .ReturnsAsync(cafe);
        _orderRepositoryMock
            .Setup(x => x.UpdateStatus(order.Id, OrderStatus.Completed))
            .Returns(Task.CompletedTask);

        // Act
        await _orderService.CompleteOrder(cafeAdmin.Id, order.Id);

        // Assert
        _orderRepositoryMock.Verify(x => x.UpdateStatus(order.Id, OrderStatus.Completed), Times.Once);
        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Once);
    }

    [Fact]
    public async Task CompleteOrder_WithNonExistingOrder_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var orderId = 1;
        var expectedException = new OrderNotFoundException(
            $"Order with id: {orderId} not found",
            orderId);
        _orderRepositoryMock
            .Setup(x => x.GetById(orderId))
            .ReturnsAsync(default(Order));

        // Act
        var act = async () => await _orderService.CompleteOrder(cafeAdmin.Id, orderId);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(orderId), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Never);
    }

    [Fact]
    public async Task CompleteOrder_WithOrderNotInProgress_ShouldThrowInvalidOrderStatusException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var order = OrderFaker.Generate()
            .WithStatus(OrderStatus.Pending);
        var expectedStatus = OrderStatus.InProgress;
        var expectedException = new InvalidOrderStatusException(
            $"Order with id: {order.Id} is not in {expectedStatus} status",
            expectedStatus,
            order.Id);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);

        // Act
        var act = async () => await _orderService.CompleteOrder(cafeAdmin.Id, order.Id);

        // Assert
        await act.Should().ThrowAsync<InvalidOrderStatusException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Never);
    }

    [Fact]
    public async Task CompleteOrder_WithOrderFromDifferentCafe_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate();
        var cafe = CafeFaker.Generate()[0]
            .WithId(1);
        var otherCafe = CafeFaker.Generate()[0]
            .WithAdmins(cafeAdmin)
            .WithId(2);
        var order = OrderFaker.Generate()
            .WithCafe(otherCafe)
            .WithStatus(OrderStatus.InProgress);
        var expectedException = new OrderNotFoundException(
            $"Order with id: {order.Id} not found in cafe with id: {cafe.Id}",
            order.Id);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);
        _cafeServiceMock
            .Setup(x => x.GetByAdminId(cafeAdmin[0].Id))
            .ReturnsAsync(cafe);

        // Act
        var act = async () => await _orderService.CompleteOrder(cafeAdmin[0].Id, order.Id);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin[0].Id), Times.Once);
    }

    [Fact]
    public async Task CafeCancelOrder_WithExistingOrderNotCompleted_ShouldCancelOrder()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var cafe = CafeFaker.Generate()[0]
            .WithId(1);
        var order = OrderFaker.Generate()
            .WithCafe(cafe)
            .WithStatus(OrderStatus.Pending);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);
        _cafeServiceMock
            .Setup(x => x.GetByAdminId(cafeAdmin.Id))
            .ReturnsAsync(cafe);
        _orderRepositoryMock
            .Setup(x => x.UpdateStatus(order.Id, OrderStatus.Cancelled))
            .Returns(Task.CompletedTask);

        // Act
        await _orderService.CafeCancelOrder(cafeAdmin.Id, order.Id);

        // Assert
        _orderRepositoryMock.Verify(x => x.UpdateStatus(order.Id, OrderStatus.Cancelled), Times.Once);
        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Once);
    }

    [Fact]
    public async Task CafeCancelOrder_WithNonExistingOrder_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var orderId = 1;
        var expectedException = new OrderNotFoundException(
            $"Order with id: {orderId} not found",
            orderId);
        _orderRepositoryMock
            .Setup(x => x.GetById(orderId))
            .ReturnsAsync(default(Order));

        // Act
        var act = async () => await _orderService.CafeCancelOrder(cafeAdmin.Id, orderId);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(orderId), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Never);
    }

    [Fact]
    public async Task CafeCancelOrder_WithOrderCompleted_ShouldThrowInvalidOrderStatusException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var order = OrderFaker.Generate()
            .WithStatus(OrderStatus.Completed);
        var expectedException = new InvalidOrderStatusException(
            $"Order with id: {order.Id} is already completed",
            OrderStatus.Completed,
            order.Id);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);

        // Act
        var act = async () => await _orderService.CafeCancelOrder(cafeAdmin.Id, order.Id);

        // Assert
        await act.Should().ThrowAsync<InvalidOrderStatusException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Never);
    }

    [Fact]
    public async Task CafeCancelOrder_WithOrderFromDifferentCafe_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate();
        var cafe = CafeFaker.Generate()[0]
            .WithId(1);
        var otherCafe = CafeFaker.Generate()[0]
            .WithAdmins(cafeAdmin)
            .WithId(2);
        var order = OrderFaker.Generate()
            .WithCafe(otherCafe)
            .WithStatus(OrderStatus.Pending);
        var expectedException = new OrderNotFoundException(
            $"Order with id: {order.Id} not found in cafe with id: {cafe.Id}",
            order.Id);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);
        _cafeServiceMock
            .Setup(x => x.GetByAdminId(cafeAdmin[0].Id))
            .ReturnsAsync(cafe);

        // Act
        var act = async () => await _orderService.CafeCancelOrder(cafeAdmin[0].Id, order.Id);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin[0].Id), Times.Once);
    }

    [Fact]
    public async Task CustomerCancelOrder_WithExistingOrderNotCompleted_ShouldCancelOrder()
    {
        // Arrange
        var customer = CustomerFaker.Generate()[0];
        var order = OrderFaker.Generate()
            .WithCustomer(customer)
            .WithStatus(OrderStatus.Pending);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);
        _orderRepositoryMock
            .Setup(x => x.UpdateStatus(order.Id, OrderStatus.Cancelled))
            .Returns(Task.CompletedTask);

        // Act
        await _orderService.CustomerCancelOrder(customer.Id, order.Id);

        // Assert
        _orderRepositoryMock.Verify(x => x.UpdateStatus(order.Id, OrderStatus.Cancelled), Times.Once);
        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
    }

    [Fact]
    public async Task CustomerCancelOrder_WithNonExistingOrder_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var customerId = "1";
        var orderId = 1;
        var expectedException = new OrderNotFoundException(
            $"Order with id: {orderId} not found",
            orderId);
        _orderRepositoryMock
            .Setup(x => x.GetById(orderId))
            .ReturnsAsync(default(Order));

        // Act
        var act = async () => await _orderService.CustomerCancelOrder(customerId, orderId);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(orderId), Times.Once);
    }

    [Fact]
    public async Task CustomerCancelOrder_WithOrderCompleted_ShouldThrowInvalidOrderStatusException()
    {
        // Arrange
        var customer = CustomerFaker.Generate()[0];
        var order = OrderFaker.Generate()
            .WithCustomer(customer)
            .WithStatus(OrderStatus.Completed);
        var expectedException = new InvalidOrderStatusException(
            $"Order with id: {order.Id} is already completed",
            OrderStatus.Completed,
            order.Id);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);

        // Act
        var act = async () => await _orderService.CustomerCancelOrder(customer.Id, order.Id);

        // Assert
        await act.Should().ThrowAsync<InvalidOrderStatusException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
    }

    [Fact]
    public async Task CustomerCancelOrder_WithOrderFromDifferentCustomer_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var customer = CustomerFaker.Generate()[0];
        var otherCustomer = CustomerFaker.Generate()[0];
        var order = OrderFaker.Generate()
            .WithCustomer(otherCustomer)
            .WithStatus(OrderStatus.Pending);
        var expectedException = new OrderNotFoundException(
            $"Order with id: {order.Id} not found for user with id: {customer.Id}",
            order.Id);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);

        // Act
        var act = async () => await _orderService.CustomerCancelOrder(customer.Id, order.Id);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
    }

    [Fact]
    public async Task PayOrder_WithExistingOrderPending_ShouldPayOrder()
    {
        // Arrange
        var getId = "1";
        var order = OrderFaker.Generate()
            .WithStatus(OrderStatus.Pending);
        var totalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.PricePerItem);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);
        _paymentServiceMock
            .Setup(x => x.ProcessPayment(getId, totalAmount))
            .Returns(Task.CompletedTask);
        _orderRepositoryMock
            .Setup(x => x.UpdateStatus(order.Id, OrderStatus.InProgress))
            .Returns(Task.CompletedTask);

        // Act
        await _orderService.PayOrder(getId, order.Id);

        // Assert
        _orderRepositoryMock.Verify(x => x.UpdateStatus(order.Id, OrderStatus.InProgress), Times.Once);
        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _paymentServiceMock.Verify(x => x.ProcessPayment(getId, totalAmount), Times.Once);
    }

    [Fact]
    public async Task PayOrder_WithNonExistingOrder_ShouldThrowOrderNotFoundException()
    {
        // Arrange
        var getId = "1";
        var orderId = 1;
        var expectedException = new OrderNotFoundException(
            $"Order with id: {orderId} not found",
            orderId);
        _orderRepositoryMock
            .Setup(x => x.GetById(orderId))
            .ReturnsAsync(default(Order));

        // Act
        var act = async () => await _orderService.PayOrder(getId, orderId);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(orderId), Times.Once);
        _paymentServiceMock.Verify(x => x.ProcessPayment(getId, It.IsAny<decimal>()), Times.Never);
    }

    [Fact]
    public async Task PayOrder_WithOrderNotPending_ShouldThrowInvalidOrderStatusException()
    {
        // Arrange
        var getId = "1";
        var order = OrderFaker.Generate()
            .WithStatus(OrderStatus.InProgress);
        var expectedException = new InvalidOrderStatusException(
            $"Order with id: {order.Id} is not in {OrderStatus.Pending} status",
            OrderStatus.Pending,
            order.Id);
        _orderRepositoryMock
            .Setup(x => x.GetById(order.Id))
            .ReturnsAsync(order);

        // Act
        var act = async () => await _orderService.PayOrder(getId, order.Id);

        // Assert
        await act.Should().ThrowAsync<InvalidOrderStatusException>()
            .WithMessage(expectedException.Message);

        _orderRepositoryMock.Verify(x => x.GetById(order.Id), Times.Once);
        _paymentServiceMock.Verify(x => x.ProcessPayment(getId, It.IsAny<decimal>()), Times.Never);
    }
}