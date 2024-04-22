using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.UnitTests.Fakers;
using FluentAssertions;
using Moq;

namespace CoffeeScoutBackend.UnitTests.Tests;

public class BeverageTypeServiceTests
{
    private readonly Mock<IBeverageTypeRepository> _beverageTypeRepositoryFake = new(MockBehavior.Strict);
    private readonly IBeverageTypeService _beverageTypeService;

    public BeverageTypeServiceTests()
    {
        _beverageTypeService = new BeverageTypeService(_beverageTypeRepositoryFake.Object);
    }

    [Fact]
    public async Task GetById_WhenBeverageTypeExists_ReturnsBeverageType()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];

        _beverageTypeRepositoryFake
            .Setup(x => x.GetById(beverageType.Id))
            .ReturnsAsync(beverageType);

        // Act
        var result = await _beverageTypeService.GetById(beverageType.Id);

        // Assert
        result.Should().BeEquivalentTo(beverageType);

        _beverageTypeRepositoryFake.Verify(x => x.GetById(beverageType.Id), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenBeverageTypeDoesNotExist_ThrowsBeverageTypeNotFoundException()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];

        _beverageTypeRepositoryFake
            .Setup(x => x.GetById(beverageType.Id))
            .ReturnsAsync(default(BeverageType));

        // Act
        Func<Task> act = async () => await _beverageTypeService.GetById(beverageType.Id);

        // Assert
        await act.Should().ThrowAsync<BeverageTypeNotFoundException>()
            .WithMessage($"Beverage type with id {beverageType.Id} not found");

        _beverageTypeRepositoryFake.Verify(x => x.GetById(beverageType.Id), Times.Once);
    }

    [Fact]
    public async Task Add_ReturnsAddedBeverageType()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];

        _beverageTypeRepositoryFake
            .Setup(x => x.Add(beverageType))
            .ReturnsAsync(beverageType with { Id = 1 });

        // Act
        var result = await _beverageTypeService.Add(beverageType);

        // Assert
        result.Should().BeEquivalentTo(beverageType with { Id = 1 });

        _beverageTypeRepositoryFake.Verify(x => x.Add(beverageType), Times.Once);
    }

    [Fact]
    public async Task Update_WhenBeverageTypeExists_UpdatesBeverageType()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];
        var updatedBeverageTypeData = new BeverageType
        {
            Name = "Updated Name",
            Description = "Updated Description"
        };
        var updatedBeverageType = updatedBeverageTypeData with { Id = beverageType.Id };

        _beverageTypeRepositoryFake
            .Setup(x => x.Update(updatedBeverageType))
            .Returns(Task.CompletedTask);
        _beverageTypeRepositoryFake
            .Setup(x => x.GetById(beverageType.Id))
            .ReturnsAsync(beverageType);

        // Act
        await _beverageTypeService.Update(beverageType.Id, updatedBeverageTypeData);

        // Assert
        _beverageTypeRepositoryFake.Verify(x => x.Update(updatedBeverageType), Times.Once);
        _beverageTypeRepositoryFake.Verify(x => x.GetById(beverageType.Id), Times.Once);
    }

    [Fact]
    public async Task Update_WhenBeverageTypeDoesNotExist_ThrowsBeverageTypeNotFoundException()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];
        var updatedBeverageTypeData = new BeverageType
        {
            Name = "Updated Name",
            Description = "Updated Description"
        };

        _beverageTypeRepositoryFake
            .Setup(x => x.GetById(beverageType.Id))
            .ReturnsAsync(default(BeverageType));

        // Act
        Func<Task> act = async () => await _beverageTypeService.Update(beverageType.Id, updatedBeverageTypeData);

        // Assert
        await act.Should().ThrowAsync<BeverageTypeNotFoundException>()
            .WithMessage($"Beverage type with id {beverageType.Id} not found");

        _beverageTypeRepositoryFake.Verify(x => x.GetById(beverageType.Id), Times.Once);
        _beverageTypeRepositoryFake.Verify(x => x.Update(It.IsAny<BeverageType>()), Times.Never);
    }

    [Fact]
    public async Task Delete_WhenBeverageTypeExists_DeletesBeverageType()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];

        _beverageTypeRepositoryFake
            .Setup(x => x.GetById(beverageType.Id))
            .ReturnsAsync(beverageType);
        _beverageTypeRepositoryFake
            .Setup(x => x.Delete(beverageType.Id))
            .Returns(Task.CompletedTask);

        // Act
        await _beverageTypeService.Delete(beverageType.Id);

        // Assert
        _beverageTypeRepositoryFake.Verify(x => x.GetById(beverageType.Id), Times.Once);
        _beverageTypeRepositoryFake.Verify(x => x.Delete(beverageType.Id), Times.Once);
    }

    [Fact]
    public async Task Delete_WhenBeverageTypeDoesNotExist_ThrowsBeverageTypeNotFoundException()
    {
        // Arrange
        var beverageType = BeverageTypeFaker.Generate()[0];

        _beverageTypeRepositoryFake
            .Setup(x => x.GetById(beverageType.Id))
            .ReturnsAsync(default(BeverageType));

        // Act
        Func<Task> act = async () => await _beverageTypeService.Delete(beverageType.Id);

        // Assert
        await act.Should().ThrowAsync<BeverageTypeNotFoundException>()
            .WithMessage($"Beverage type with id {beverageType.Id} not found");

        _beverageTypeRepositoryFake.Verify(x => x.GetById(beverageType.Id), Times.Once);
        _beverageTypeRepositoryFake.Verify(x => x.Delete(It.IsAny<long>()), Times.Never);
    }

    [Fact]
    public async Task GetPage_ReturnsPageOfBeverageTypes()
    {
        // Arrange
        const int pageSize = 3;
        const int pageNumber = 1;
        var beverageTypes = BeverageTypeFaker.Generate(pageSize);

        _beverageTypeRepositoryFake
            .Setup(x => x.GetPage(pageSize, pageNumber))
            .ReturnsAsync(beverageTypes);

        // Act
        var result = await _beverageTypeService.GetPage(pageSize, pageNumber);

        // Assert
        result.Should().BeEquivalentTo(beverageTypes);

        _beverageTypeRepositoryFake.Verify(x => x.GetPage(pageSize, pageNumber), Times.Once);
    }
}