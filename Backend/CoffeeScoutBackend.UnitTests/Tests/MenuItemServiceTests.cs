using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.UnitTests.Fakers;
using FluentAssertions;
using Moq;

namespace CoffeeScoutBackend.UnitTests.Tests;

public class MenuItemServiceTests
{
    private readonly Mock<IMenuItemRepository> _menuItemRepositoryMock = new(MockBehavior.Strict);
    private readonly Mock<ICafeService> _cafeServiceMock = new(MockBehavior.Strict);
    private readonly Mock<IBeverageTypeService> _beverageTypeServiceMock = new(MockBehavior.Strict);
    private readonly IMenuItemService _menuItemService;

    public MenuItemServiceTests()
    {
        _menuItemService = new MenuItemService(
            _menuItemRepositoryMock.Object,
            _beverageTypeServiceMock.Object,
            _cafeServiceMock.Object);
    }

    [Fact]
    public async Task GetById_WhenMenuItemExists_ReturnsMenuItem()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        _menuItemRepositoryMock
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(menuItem);

        // Act
        var result = await _menuItemService.GetById(menuItem.Id);

        // Assert
        result.Should().BeEquivalentTo(menuItem);

        _menuItemRepositoryMock.Verify(x => x.GetById(menuItem.Id), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenMenuItemDoesNotExist_ThrowsMenuItemNotFoundException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        _menuItemRepositoryMock
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(default(MenuItem));

        // Act
        Func<Task> act = async () => await _menuItemService.GetById(menuItem.Id);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage($"Menu item with id:{menuItem.Id} not found");

        _menuItemRepositoryMock.Verify(x => x.GetById(menuItem.Id), Times.Once);
    }

    [Fact]
    public async Task GetByCafeId_WhenCafeExists_ReturnsMenuItems()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var menuItems = MenuItemFaker.Generate(3);
        cafe.MenuItems = menuItems;

        _cafeServiceMock
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(cafe);

        // Act
        var result = await _menuItemService.GetByCafeId(cafe.Id);

        // Assert
        result.Should().BeEquivalentTo(menuItems);

        _cafeServiceMock.Verify(x => x.GetById(cafe.Id), Times.Once);
    }

    [Fact]
    public async Task GetByCafeId_WhenCafeDoesNotExist_ThrowsCafeNotFoundException()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var expectedException = new CafeNotFoundException($"Cafe with id:{cafe.Id} not found", cafe.Id);

        _cafeServiceMock
            .Setup(x => x.GetById(cafe.Id))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _menuItemService.GetByCafeId(cafe.Id);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage(expectedException.Message);

        _cafeServiceMock.Verify(x => x.GetById(cafe.Id), Times.Once);
    }

    [Fact]
    public async Task GetAllInAreaByBeverageType_WhenBeverageTypeExists_ReturnsMenuItems()
    {
        // Arrange
        var location = LocationFaker.Generate()[0];
        var radiusInMeters = 1000;
        var beverageType = BeverageTypeFaker.Generate()[0];
        var menuItems = MenuItemFaker.Generate(3);

        _beverageTypeServiceMock
            .Setup(x => x.GetById(beverageType.Id))
            .ReturnsAsync(beverageType);

        _menuItemRepositoryMock
            .Setup(x => x.GetAllInAreaByBeverageType(location, radiusInMeters, beverageType))
            .ReturnsAsync(menuItems);

        // Act
        var result = await _menuItemService.GetAllInAreaByBeverageType(location, radiusInMeters, beverageType.Id);

        // Assert
        result.Should().BeEquivalentTo(menuItems);

        _beverageTypeServiceMock.Verify(x => x.GetById(beverageType.Id), Times.Once);
        _menuItemRepositoryMock.Verify(x => x.GetAllInAreaByBeverageType(location, radiusInMeters, beverageType),
            Times.Once);
    }

    [Fact]
    public async Task GetAllInAreaByBeverageType_WhenBeverageTypeDoesNotExist_ThrowsBeverageTypeNotFoundException()
    {
        // Arrange
        var location = LocationFaker.Generate()[0];
        var radiusInMeters = 1000;
        var beverageType = BeverageTypeFaker.Generate()[0];
        var expectedException =
            new BeverageTypeNotFoundException($"Beverage type with id:{beverageType.Id} not found", beverageType.Id);

        _beverageTypeServiceMock
            .Setup(x => x.GetById(beverageType.Id))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () =>
            await _menuItemService.GetAllInAreaByBeverageType(location, radiusInMeters, beverageType.Id);

        // Assert
        await act.Should().ThrowAsync<BeverageTypeNotFoundException>()
            .WithMessage(expectedException.Message);

        _beverageTypeServiceMock.Verify(x => x.GetById(beverageType.Id), Times.Once);
    }

    [Fact]
    public async Task Add_WhenBeverageTypeAndCafeExists_AddsMenuItem()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate();
        cafe.WithAdmins(cafeAdmin);
        cafeAdmin[0] = cafeAdmin[0].WithCafe(cafe);
        var menuItem = MenuItemFaker.Generate()[0];

        var model = new AddMenuItemModel
        {
            BeverageTypeId = beverageType.Id,
            CafeAdminId = cafeAdmin[0].Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
            SizeInMl = menuItem.SizeInMl
        };

        _beverageTypeServiceMock
            .Setup(x => x.GetById(model.BeverageTypeId))
            .ReturnsAsync(beverageType);

        _cafeServiceMock
            .Setup(x => x.GetByAdminId(model.CafeAdminId))
            .ReturnsAsync(cafe);

        _menuItemRepositoryMock
            .Setup(x => x.Add(It.IsAny<MenuItem>()))
            .ReturnsAsync(menuItem);

        // Act
        var result = await _menuItemService.Add(model);

        // Assert
        result.Should().BeEquivalentTo(menuItem);

        _beverageTypeServiceMock.Verify(x => x.GetById(model.BeverageTypeId), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(model.CafeAdminId), Times.Once);
        _menuItemRepositoryMock.Verify(x => x.Add(It.IsAny<MenuItem>()), Times.Once);
    }

    [Fact]
    public async Task Add_WhenBeverageTypeDoesNotExist_ThrowsBeverageTypeNotFoundException()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate();
        cafe.WithAdmins(cafeAdmin);
        cafeAdmin[0] = cafeAdmin[0].WithCafe(cafe);
        var menuItem = MenuItemFaker.Generate()[0];
        var expectedException =
            new BeverageTypeNotFoundException($"Beverage type with id:{beverageType.Id} not found", beverageType.Id);

        var model = new AddMenuItemModel
        {
            BeverageTypeId = beverageType.Id,
            CafeAdminId = cafeAdmin[0].Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
            SizeInMl = menuItem.SizeInMl
        };

        _beverageTypeServiceMock
            .Setup(x => x.GetById(model.BeverageTypeId))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _menuItemService.Add(model);

        // Assert
        await act.Should().ThrowAsync<BeverageTypeNotFoundException>()
            .WithMessage(expectedException.Message);

        _beverageTypeServiceMock.Verify(x => x.GetById(model.BeverageTypeId), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(model.CafeAdminId), Times.Never);
        _menuItemRepositoryMock.Verify(x => x.Add(It.IsAny<MenuItem>()), Times.Never);
    }

    [Fact]
    public async Task Add_WhenCafeDoesNotExist_ThrowsCafeNotFoundException()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate();
        cafe.WithAdmins(cafeAdmin);
        cafeAdmin[0] = cafeAdmin[0].WithCafe(cafe);
        var menuItem = MenuItemFaker.Generate()[0];
        var expectedException = new CafeNotFoundException($"Cafe for admin with id: {cafeAdmin[0].Id} not found");

        var model = new AddMenuItemModel
        {
            BeverageTypeId = beverageType.Id,
            CafeAdminId = cafeAdmin[0].Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
            SizeInMl = menuItem.SizeInMl
        };

        _beverageTypeServiceMock
            .Setup(x => x.GetById(model.BeverageTypeId))
            .ReturnsAsync(beverageType);

        _cafeServiceMock
            .Setup(x => x.GetByAdminId(model.CafeAdminId))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _menuItemService.Add(model);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage(expectedException.Message);

        _beverageTypeServiceMock.Verify(x => x.GetById(model.BeverageTypeId), Times.Once);
        _cafeServiceMock.Verify(x => x.GetByAdminId(model.CafeAdminId), Times.Once);
        _menuItemRepositoryMock.Verify(x => x.Add(It.IsAny<MenuItem>()), Times.Never);
    }

    [Fact]
    public async Task Update_WhenMenuItemExists_UpdatesMenuItem()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var beverageType = BeverageTypeFaker.Generate()[0];
        var model = new UpdateMenuItemModel
        {
            Id = menuItem.Id,
            BeverageTypeId = beverageType.Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
            SizeInMl = menuItem.SizeInMl
        };

        _menuItemRepositoryMock
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(menuItem);

        _beverageTypeServiceMock
            .Setup(x => x.GetById(model.BeverageTypeId))
            .ReturnsAsync(beverageType);

        _menuItemRepositoryMock
            .Setup(x => x.Update(It.IsAny<MenuItem>()))
            .Returns(Task.CompletedTask);

        // Act
        await _menuItemService.Update(model);

        // Assert
        _menuItemRepositoryMock.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _beverageTypeServiceMock.Verify(x => x.GetById(model.BeverageTypeId), Times.Once);
        _menuItemRepositoryMock.Verify(x => x.Update(It.IsAny<MenuItem>()), Times.Once);
    }

    [Fact]
    public async Task Update_WhenMenuItemDoesNotExist_ThrowsMenuItemNotFoundException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var beverageType = BeverageTypeFaker.Generate()[0];
        var model = new UpdateMenuItemModel
        {
            Id = menuItem.Id,
            BeverageTypeId = beverageType.Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
            SizeInMl = menuItem.SizeInMl
        };
        var expectedException = new MenuItemNotFoundException($"Menu item with id:{menuItem.Id} not found", menuItem
            .Id);

        _menuItemRepositoryMock
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(default(MenuItem));

        // Act
        Func<Task> act = async () => await _menuItemService.Update(model);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage(expectedException.Message);

        _menuItemRepositoryMock.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _beverageTypeServiceMock.Verify(x => x.GetById(model.BeverageTypeId), Times.Never);
        _menuItemRepositoryMock.Verify(x => x.Update(It.IsAny<MenuItem>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_WhenBeverageTypeDoesNotExist_ThrowsBeverageTypeNotFoundException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var beverageType = BeverageTypeFaker.Generate()[0];
        var model = new UpdateMenuItemModel
        {
            Id = menuItem.Id,
            BeverageTypeId = beverageType.Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
            SizeInMl = menuItem.SizeInMl
        };
        var expectedException =
            new BeverageTypeNotFoundException($"Beverage type with id:{beverageType.Id} not found", beverageType.Id);

        _menuItemRepositoryMock
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(menuItem);

        _beverageTypeServiceMock
            .Setup(x => x.GetById(model.BeverageTypeId))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _menuItemService.Update(model);

        // Assert
        await act.Should().ThrowAsync<BeverageTypeNotFoundException>()
            .WithMessage(expectedException.Message);

        _menuItemRepositoryMock.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _beverageTypeServiceMock.Verify(x => x.GetById(model.BeverageTypeId), Times.Once);
        _menuItemRepositoryMock.Verify(x => x.Update(It.IsAny<MenuItem>()), Times.Never);
    }
    
    [Fact]
    public async Task GetByCafeAdmin_ReturnsMenuItems()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var cafe = CafeFaker.Generate()[0];
        var menuItems = MenuItemFaker.Generate(3);
        cafe.MenuItems = menuItems;
        
        _cafeServiceMock
            .Setup(x => x.GetByAdminId(cafeAdmin.Id))
            .ReturnsAsync(cafe);

        // Act
        var result = await _menuItemService.GetByCafeAdmin(cafeAdmin.Id);

        // Assert
        result.Should().BeEquivalentTo(menuItems);

        _cafeServiceMock.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Once);
    }
    
    [Fact]
    public async Task Delete_WhenMenuItemExists_DeletesMenuItem()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        
        _menuItemRepositoryMock
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(menuItem);

        _menuItemRepositoryMock
            .Setup(x => x.Delete(menuItem.Id))
            .Returns(Task.CompletedTask);

        // Act
        await _menuItemService.Delete(menuItem.Id);

        // Assert
        _menuItemRepositoryMock.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _menuItemRepositoryMock.Verify(x => x.Delete(menuItem.Id), Times.Once);
    }
    
    [Fact]
    public async Task Delete_WhenMenuItemDoesNotExist_ThrowsMenuItemNotFoundException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var expectedException = new MenuItemNotFoundException($"Menu item with id:{menuItem.Id} not found", menuItem
            .Id);

        _menuItemRepositoryMock
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(default(MenuItem));

        // Act
        Func<Task> act = async () => await _menuItemService.Delete(menuItem.Id);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage(expectedException.Message);

        _menuItemRepositoryMock.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _menuItemRepositoryMock.Verify(x => x.Delete(menuItem.Id), Times.Never);
    }
    
    [Fact]
    public async Task Search_WhenMenuItemsExist_ReturnsMenuItems()
    {
        // Arrange
        var searchQuery = "coffee";
        const int limit = 3;
        var menuItems = MenuItemFaker.Generate(3);
        
        _menuItemRepositoryMock
            .Setup(x => x.Search(searchQuery, limit))
            .ReturnsAsync(menuItems);

        // Act
        var result = await _menuItemService.Search(searchQuery, limit);

        // Assert
        result.Should().BeEquivalentTo(menuItems);

        _menuItemRepositoryMock.Verify(x => x.Search(searchQuery, limit), Times.Once);
    }
}