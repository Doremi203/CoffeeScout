using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Dal.Entities;
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

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryFake = new(MockBehavior.Strict);

    private readonly ICustomerService _customerService;
    private readonly Mock<IMenuItemService> _menuItemServiceFake = new(MockBehavior.Strict);
    private readonly Mock<IRoleRegistrationService> _roleRegistrationServiceFake = new(MockBehavior.Strict);

    public CustomerServiceTests()
    {
        _customerService = new CustomerService(
            _customerRepositoryFake.Object,
            _menuItemServiceFake.Object,
            _roleRegistrationServiceFake.Object
        );
    }

    [Fact]
    public async Task RegisterCustomer_WithValidData_ShouldRegisterCustomer()
    {
        // Arrange
        var customerRegistrationData = CustomerRegistrationDataFaker.Generate().First();
        var customer = CustomerFaker.Generate().First()
            .WithFirstName(customerRegistrationData.FirstName);
        var appUser = new AppUser
        {
            Id = customer.Id,
            UserName = customerRegistrationData.Email,
            Email = customerRegistrationData.Email
        };

        _roleRegistrationServiceFake
            .Setup(x => x.RegisterUser(It.IsAny<AppUser>(), customerRegistrationData.Password, Roles.Customer))
            .ReturnsAsync(appUser);
        _customerRepositoryFake
            .Setup(x => x.Add(It.IsAny<Customer>()))
            .Returns(Task.CompletedTask);

        // Act
        await _customerService.RegisterCustomer(customerRegistrationData);

        // Assert
        _roleRegistrationServiceFake.Verify(
            x => x.RegisterUser(It.IsAny<AppUser>(), customerRegistrationData.Password, Roles.Customer), Times.Once);
        _customerRepositoryFake.Verify(x => x.Add(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async Task GetByUserId_WithExistingCustomer_ShouldReturnCustomer()
    {
        // Arrange
        var customer = CustomerFaker.Generate().First();

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);

        // Act
        var result = await _customerService.GetByUserId(customer.Id);

        // Assert
        result.Should().BeEquivalentTo(customer);

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
    }

    [Fact]
    public async Task GetByUserId_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();

        _customerRepositoryFake
            .Setup(x => x.GetById(userId))
            .ReturnsAsync(default(Customer));

        // Act
        Func<Task> act = async () => await _customerService.GetByUserId(userId);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage($"Customer with id:{userId} not found");

        _customerRepositoryFake.Verify(x => x.GetById(userId), Times.Once);
    }

    [Fact]
    public async Task AddFavoredMenuItem_WithExistingCustomerAndMenuItem_ShouldAddMenuItemToFavored()
    {
        // Arrange
        var customer = CustomerFaker.Generate().First();
        var menuItem = MenuItemFaker.Generate().First();
        var menuItemId = menuItem.Id;

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItemId))
            .ReturnsAsync(menuItem);
        _customerRepositoryFake
            .Setup(x => x.AddFavoredMenuItem(customer, menuItem))
            .Returns(Task.CompletedTask);

        // Act
        await _customerService.AddFavoredMenuItem(customer.Id, menuItemId);

        // Assert
        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
        _menuItemServiceFake.Verify(x => x.GetById(menuItemId), Times.Once);
        _customerRepositoryFake.Verify(x => x.AddFavoredMenuItem(customer, menuItem), Times.Once);
    }

    [Fact]
    public async Task
        AddFavoredMenuItem_WithExistingCustomerAndAlreadyFavoredMenuItem_ShouldThrowMenuItemAlreadyFavoredException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate().First();
        var customer = CustomerFaker.Generate().First()
            .WithFavoriteMenuItems([menuItem]);
        var menuItemId = menuItem.Id;

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItemId))
            .ReturnsAsync(menuItem);

        // Act
        var act = async () => await _customerService.AddFavoredMenuItem(customer.Id, menuItemId);

        // Assert
        await act.Should().ThrowAsync<MenuItemAlreadyFavoredException>()
            .WithMessage($"Menu item with id:{menuItemId} is already favored by customer with id:{customer.Id}");

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
        _menuItemServiceFake.Verify(x => x.GetById(menuItemId), Times.Once);
    }

    [Fact]
    public async Task AddFavoredMenuItem_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var menuItem = MenuItemFaker.Generate().First();
        var menuItemId = menuItem.Id;

        _customerRepositoryFake
            .Setup(x => x.GetById(userId))
            .ReturnsAsync(default(Customer));

        // Act
        var act = async () => await _customerService.AddFavoredMenuItem(userId, menuItemId);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage($"Customer with id:{userId} not found");

        _customerRepositoryFake.Verify(x => x.GetById(userId), Times.Once);
    }

    [Fact]
    public async Task AddFavoredMenuItem_WithNonExistingMenuItem_ShouldThrowMenuItemNotFoundException()
    {
        // Arrange
        var customer = CustomerFaker.Generate().First();
        var menuItemId = 1L;
        var expectedException = new MenuItemNotFoundException(
            $"Menu item with id:{menuItemId} not found", menuItemId);

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItemId))
            .ThrowsAsync(expectedException);

        // Act
        var act = async () => await _customerService.AddFavoredMenuItem(customer.Id, menuItemId);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage(expectedException.Message);

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
        _menuItemServiceFake.Verify(x => x.GetById(menuItemId), Times.Once);
    }

    [Fact]
    public async Task GetFavoredMenuItems_WithExistingCustomer_ShouldReturnFavoredMenuItems()
    {
        // Arrange
        var favoredMenuItems = MenuItemFaker.Generate(3);
        var customer = CustomerFaker.Generate().First()
            .WithFavoriteMenuItems(favoredMenuItems);

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);

        // Act
        var result = await _customerService.GetFavoredMenuItems(customer.Id);

        // Assert
        result.Should().BeEquivalentTo(favoredMenuItems);

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
    }

    [Fact]
    public async Task GetFavoredMenuItems_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();

        _customerRepositoryFake
            .Setup(x => x.GetById(userId))
            .ReturnsAsync(default(Customer));

        // Act
        Func<Task> act = async () => await _customerService.GetFavoredMenuItems(userId);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage($"Customer with id:{userId} not found");

        _customerRepositoryFake.Verify(x => x.GetById(userId), Times.Once);
    }

    [Fact]
    public async Task RemoveFavoredMenuItem_WithExistingCustomerAndFavoredMenuItem_ShouldRemoveFavoredMenuItem()
    {
        // Arrange
        var favoredMenuItem = MenuItemFaker.Generate().First();
        var customer = CustomerFaker.Generate().First()
            .WithFavoriteMenuItems([favoredMenuItem]);
        var menuItemId = favoredMenuItem.Id;

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItemId))
            .ReturnsAsync(favoredMenuItem);
        _customerRepositoryFake
            .Setup(x => x.RemoveFavoredMenuItem(customer, favoredMenuItem))
            .Returns(Task.CompletedTask);

        // Act
        await _customerService.RemoveFavoredMenuItem(customer.Id, menuItemId);

        // Assert
        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
        _menuItemServiceFake.Verify(x => x.GetById(menuItemId), Times.Once);
        _customerRepositoryFake.Verify(x => x.RemoveFavoredMenuItem(customer, favoredMenuItem), Times.Once);
    }

    [Fact]
    public async Task
        RemoveFavoredMenuItem_WithExistingCustomerAndNonFavoredMenuItem_ShouldThrowFavoredMenuItemNotFoundException()
    {
        // Arrange
        var favoredMenuItem = MenuItemFaker.Generate().First();
        var customer = CustomerFaker.Generate().First();
        var menuItemId = favoredMenuItem.Id;

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItemId))
            .ReturnsAsync(favoredMenuItem);

        // Act
        var act = async () => await _customerService.RemoveFavoredMenuItem(customer.Id, menuItemId);

        // Assert
        await act.Should().ThrowAsync<FavoredMenuItemNotFoundException>()
            .WithMessage($"Menu item with id:{menuItemId} is not favored by customer with id:{customer.Id}");

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
        _menuItemServiceFake.Verify(x => x.GetById(menuItemId), Times.Once);
    }

    [Fact]
    public async Task RemoveFavoredMenuItem_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var menuItemId = 1L;

        _customerRepositoryFake
            .Setup(x => x.GetById(userId))
            .ReturnsAsync(default(Customer));

        // Act
        var act = async () => await _customerService.RemoveFavoredMenuItem(userId, menuItemId);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage($"Customer with id:{userId} not found");

        _customerRepositoryFake.Verify(x => x.GetById(userId), Times.Once);
    }

    [Fact]
    public async Task RemoveFavoredMenuItem_WithNonExistingMenuItem_ShouldThrowMenuItemNotFoundException()
    {
        // Arrange
        var customer = CustomerFaker.Generate().First();
        var menuItemId = 1L;
        var expectedException = new MenuItemNotFoundException(
            $"Menu item with id:{menuItemId} not found", menuItemId);

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItemId))
            .ThrowsAsync(expectedException);

        // Act
        var act = async () => await _customerService.RemoveFavoredMenuItem(customer.Id, menuItemId);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage(expectedException.Message);

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
        _menuItemServiceFake.Verify(x => x.GetById(menuItemId), Times.Once);
    }

    [Fact]
    public async Task GetFavoredBeverageTypes_WithExistingCustomer_ShouldReturnFavoredBeverageTypes()
    {
        // Arrange
        var favoredMenuItems = MenuItemFaker.Generate(3);
        var customer = CustomerFaker.Generate().First()
            .WithFavoriteMenuItems(favoredMenuItems);

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);

        // Act
        var result = await _customerService.GetFavoredBeverageTypes(customer.Id);

        // Assert
        result.Should().BeEquivalentTo(favoredMenuItems.Select(mi => mi.BeverageType).Distinct());

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
    }

    [Fact]
    public async Task GetFavoredBeverageTypes_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();

        _customerRepositoryFake
            .Setup(x => x.GetById(userId))
            .ReturnsAsync(default(Customer));

        // Act
        Func<Task> act = async () => await _customerService.GetFavoredBeverageTypes(userId);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage($"Customer with id:{userId} not found");

        _customerRepositoryFake.Verify(x => x.GetById(userId), Times.Once);
    }

    [Fact]
    public async Task GetInfo_WithExistingCustomer_ShouldReturnCustomerInfo()
    {
        // Arrange
        var customer = CustomerFaker.Generate().First();
        var expectedCustomerInfo = customer.Adapt<CustomerInfo>();

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);

        // Act
        var result = await _customerService.GetInfo(customer.Id);

        // Assert
        result.Should().BeEquivalentTo(expectedCustomerInfo);

        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
    }

    [Fact]
    public async Task GetInfo_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();

        _customerRepositoryFake
            .Setup(x => x.GetById(userId))
            .ReturnsAsync(default(Customer));

        // Act
        Func<Task> act = async () => await _customerService.GetInfo(userId);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage($"Customer with id:{userId} not found");

        _customerRepositoryFake.Verify(x => x.GetById(userId), Times.Once);
    }

    [Fact]
    public async Task UpdateInfo_WithExistingCustomer_ShouldUpdateCustomerInfo()
    {
        // Arrange
        var customer = CustomerFaker.Generate().First();
        var customerInfo = customer.Adapt<CustomerInfo>();

        _customerRepositoryFake
            .Setup(x => x.GetById(customer.Id))
            .ReturnsAsync(customer);
        _customerRepositoryFake
            .Setup(x => x.Update(It.IsAny<Customer>()))
            .Returns(Task.CompletedTask);

        // Act
        await _customerService.UpdateInfo(customer.Id, customerInfo);

        // Assert
        _customerRepositoryFake.Verify(x => x.GetById(customer.Id), Times.Once);
        _customerRepositoryFake.Verify(x => x.Update(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async Task UpdateInfo_WithNonExistingCustomer_ShouldThrowCustomerNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var customerInfo = CustomerFaker.Generate().First().Adapt<CustomerInfo>();

        _customerRepositoryFake
            .Setup(x => x.GetById(userId))
            .ReturnsAsync(default(Customer));

        // Act
        var act = async () => await _customerService.UpdateInfo(userId, customerInfo);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage($"Customer with id:{userId} not found");

        _customerRepositoryFake.Verify(x => x.GetById(userId), Times.Once);
    }
}