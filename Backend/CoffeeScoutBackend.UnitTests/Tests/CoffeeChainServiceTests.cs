using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.UnitTests.Fakers;
using FluentAssertions;
using Moq;

namespace CoffeeScoutBackend.UnitTests.Tests;

public class CoffeeChainServiceTests
{
    private readonly Mock<ICoffeeChainRepository> _coffeeChainRepositoryFake = new(MockBehavior.Strict);
    private readonly ICoffeeChainService _coffeeChainService;

    public CoffeeChainServiceTests()
    {
        _coffeeChainService = new CoffeeChainService(_coffeeChainRepositoryFake.Object);
    }

    [Fact]
    public async Task GetPage_ReturnsPage()
    {
        // Arrange
        var pageSize = 10;
        var pageNumber = 1;
        var expectedCoffeeChains = CoffeeChainFaker.Generate(pageSize);

        _coffeeChainRepositoryFake
            .Setup(x => x.GetPage(pageSize, pageNumber))
            .ReturnsAsync(expectedCoffeeChains);

        // Act
        var result = await _coffeeChainService.GetPage(pageSize, pageNumber);

        // Assert
        result.Should().BeEquivalentTo(expectedCoffeeChains);

        _coffeeChainRepositoryFake.Verify(x => x.GetPage(pageSize, pageNumber), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenCoffeeChainExists_ReturnsCoffeeChain()
    {
        // Arrange
        var coffeeChain = CoffeeChainFaker.Generate()[0];

        _coffeeChainRepositoryFake
            .Setup(x => x.GetById(coffeeChain.Id))
            .ReturnsAsync(coffeeChain);

        // Act
        var result = await _coffeeChainService.GetById(coffeeChain.Id);

        // Assert
        result.Should().BeEquivalentTo(coffeeChain);

        _coffeeChainRepositoryFake.Verify(x => x.GetById(coffeeChain.Id), Times.Once);
    }

    [Fact]
    public void GetById_WhenCoffeeChainDoesNotExist_ThrowsCoffeeChainNotFoundException()
    {
        // Arrange
        var coffeeChainId = 1;

        _coffeeChainRepositoryFake
            .Setup(x => x.GetById(coffeeChainId))
            .ReturnsAsync(default(CoffeeChain));

        // Act
        Func<Task> act = async () => await _coffeeChainService.GetById(coffeeChainId);

        // Assert
        act.Should().ThrowAsync<CoffeeChainNotFoundException>()
            .WithMessage($"Coffee chain with id {coffeeChainId} not found");

        _coffeeChainRepositoryFake.Verify(x => x.GetById(coffeeChainId), Times.Once);
    }

    [Fact]
    public async Task Add_ReturnsAddedCoffeeChain()
    {
        // Arrange
        var coffeeChain = CoffeeChainFaker.Generate()[0];

        _coffeeChainRepositoryFake
            .Setup(x => x.Add(coffeeChain))
            .ReturnsAsync(coffeeChain with { Id = 1 });

        // Act
        var result = await _coffeeChainService.Add(coffeeChain);

        // Assert
        result.Should().BeEquivalentTo(coffeeChain with { Id = 1 });

        _coffeeChainRepositoryFake.Verify(x => x.Add(coffeeChain), Times.Once);
    }

    [Fact]
    public async Task Update_WhenCoffeeChainExists_UpdatesCoffeeChain()
    {
        // Arrange
        var coffeeChain = CoffeeChainFaker.Generate()[0];
        var updatedCoffeeChain = CoffeeChainFaker.Generate()[0];

        _coffeeChainRepositoryFake
            .Setup(x => x.GetById(coffeeChain.Id))
            .ReturnsAsync(coffeeChain);
        _coffeeChainRepositoryFake
            .Setup(x => x.Update(coffeeChain.Id, updatedCoffeeChain))
            .Returns(Task.CompletedTask);

        // Act
        await _coffeeChainService.Update(coffeeChain.Id, updatedCoffeeChain);

        // Assert
        _coffeeChainRepositoryFake.Verify(x => x.Update(coffeeChain.Id, updatedCoffeeChain), Times.Once);
        _coffeeChainRepositoryFake.Verify(x => x.GetById(coffeeChain.Id), Times.Once);
    }

    [Fact]
    public void Update_WhenCoffeeChainDoesNotExist_ThrowsCoffeeChainNotFoundException()
    {
        // Arrange
        var coffeeChainId = 1;
        var updatedCoffeeChain = CoffeeChainFaker.Generate()[0];

        _coffeeChainRepositoryFake
            .Setup(x => x.GetById(coffeeChainId))
            .ReturnsAsync(default(CoffeeChain));

        // Act
        var act = async () => await _coffeeChainService.Update(coffeeChainId, updatedCoffeeChain);

        // Assert
        act.Should().ThrowAsync<CoffeeChainNotFoundException>()
            .WithMessage($"Coffee chain with id {coffeeChainId} not found");

        _coffeeChainRepositoryFake.Verify(x => x.GetById(coffeeChainId), Times.Once);
        _coffeeChainRepositoryFake.Verify(x => x.Update(It.IsAny<long>(), It.IsAny<CoffeeChain>()), Times.Never);
    }

    [Fact]
    public async Task Delete_WhenCoffeeChainExists_DeletesCoffeeChain()
    {
        // Arrange
        var coffeeChain = CoffeeChainFaker.Generate()[0];

        _coffeeChainRepositoryFake
            .Setup(x => x.GetById(coffeeChain.Id))
            .ReturnsAsync(coffeeChain);
        _coffeeChainRepositoryFake
            .Setup(x => x.Delete(coffeeChain.Id))
            .Returns(Task.CompletedTask);

        // Act
        await _coffeeChainService.Delete(coffeeChain.Id);

        // Assert
        _coffeeChainRepositoryFake.Verify(x => x.Delete(coffeeChain.Id), Times.Once);
        _coffeeChainRepositoryFake.Verify(x => x.GetById(coffeeChain.Id), Times.Once);
    }

    [Fact]
    public void Delete_WhenCoffeeChainDoesNotExist_ThrowsCoffeeChainNotFoundException()
    {
        // Arrange
        var coffeeChainId = 1;

        _coffeeChainRepositoryFake
            .Setup(x => x.GetById(coffeeChainId))
            .ReturnsAsync(default(CoffeeChain));

        // Act
        var act = async () => await _coffeeChainService.Delete(coffeeChainId);

        // Assert
        act.Should().ThrowAsync<CoffeeChainNotFoundException>()
            .WithMessage($"Coffee chain with id {coffeeChainId} not found");

        _coffeeChainRepositoryFake.Verify(x => x.GetById(coffeeChainId), Times.Once);
        _coffeeChainRepositoryFake.Verify(x => x.Delete(It.IsAny<long>()), Times.Never);
    }
}