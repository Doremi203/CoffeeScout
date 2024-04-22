using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.UnitTests.Fakers;
using FluentAssertions;
using Moq;

namespace CoffeeScoutBackend.UnitTests.Tests;

public class CafeServiceTests
{
    private readonly Mock<ICafeRepository> _cafeRepositoryFake = new(MockBehavior.Strict);
    private readonly ICafeService _cafeService;
    private readonly Mock<ICoffeeChainService> _coffeeChainServiceFake = new(MockBehavior.Strict);

    public CafeServiceTests()
    {
        _cafeService = new CafeService(
            _cafeRepositoryFake.Object,
            _coffeeChainServiceFake.Object
        );
    }

    [Fact]
    public async Task GetById_WhenCafeExists_ReturnsCafe()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];

        _cafeRepositoryFake
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(cafe);

        // Act
        var result = await _cafeService.GetById(cafe.Id);

        // Assert
        result.Should().BeEquivalentTo(cafe);

        _cafeRepositoryFake.Verify(x => x.GetById(cafe.Id), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenCafeDoesNotExist_ThrowsCafeNotFoundException()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];

        _cafeRepositoryFake
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(default(Cafe));

        // Act
        Func<Task> act = async () => await _cafeService.GetById(cafe.Id);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage($"Cafe with id {cafe.Id} not found");

        _cafeRepositoryFake.Verify(x => x.GetById(cafe.Id), Times.Once);
    }

    [Fact]
    public async Task GetByAdminId_WhenCafeExists_ReturnsCafe()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate()[0].WithCafe(cafe);
        cafe.WithAdmins([cafeAdmin]);

        _cafeRepositoryFake
            .Setup(x => x.GetByAdminId(cafeAdmin.Id))
            .ReturnsAsync(cafe);

        // Act
        var result = await _cafeService.GetByAdminId(cafeAdmin.Id);

        // Assert
        result.Should().BeEquivalentTo(cafe);

        _cafeRepositoryFake.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Once);
    }

    [Fact]
    public async Task GetByAdminId_WhenCafeDoesNotExist_ThrowsCafeNotFoundException()
    {
        // Arrange
        var cafeAdmin = CafeAdminFaker.Generate()[0];

        _cafeRepositoryFake
            .Setup(x => x.GetByAdminId(cafeAdmin.Id))
            .ReturnsAsync(default(Cafe));

        // Act
        Func<Task> act = async () => await _cafeService.GetByAdminId(cafeAdmin.Id);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage($"Cafe for admin with id: {cafeAdmin.Id} not found");

        _cafeRepositoryFake.Verify(x => x.GetByAdminId(cafeAdmin.Id), Times.Once);
    }

    [Fact]
    public async Task AssignNewCafeAdmin_WhenCafeExists_AssignsNewCafeAdmin()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate()[0];
        var expectedCafeAdmin = cafeAdmin.WithCafe(cafe);

        _cafeRepositoryFake
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(cafe);
        _cafeRepositoryFake
            .Setup(x => x.AddCafeAdmin(expectedCafeAdmin))
            .Returns(Task.CompletedTask);

        // Act
        await _cafeService.AssignNewCafeAdmin(cafeAdmin.Id, cafe.Id);

        // Assert
        _cafeRepositoryFake.Verify(x => x.AddCafeAdmin(expectedCafeAdmin), Times.Once);
        _cafeRepositoryFake.Verify(x => x.GetById(cafe.Id), Times.Once);
    }

    [Fact]
    public async Task AssignNewCafeAdmin_WhenCafeDoesNotExist_ThrowsCafeNotFoundException()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate()[0];

        _cafeRepositoryFake
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(default(Cafe));

        // Act
        var act = async () => await _cafeService.AssignNewCafeAdmin(cafeAdmin.Id, cafe.Id);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage($"Cafe with id {cafe.Id} not found");

        _cafeRepositoryFake.Verify(x => x.GetById(cafe.Id), Times.Once);
    }

    [Fact]
    public async Task GetCafesInArea_ReturnsCafesInArea()
    {
        // Arrange
        var location = LocationFaker.Generate()[0];
        var radius = 1000;
        var cafes = CafeFaker.Generate(3);

        _cafeRepositoryFake
            .Setup(x => x.GetCafesInArea(location, radius))
            .ReturnsAsync(cafes);

        // Act
        var result = await _cafeService.GetCafesInArea(location, radius);

        // Assert
        result.Should().BeEquivalentTo(cafes);

        _cafeRepositoryFake.Verify(x => x.GetCafesInArea(location, radius), Times.Once);
    }

    [Fact]
    public async Task AddCafe_WhenCoffeeChainExists_AddsCafe()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var coffeeChain = CoffeeChainFaker.Generate()[0];
        var expectedCafe = cafe with { CoffeeChain = coffeeChain };

        _coffeeChainServiceFake
            .Setup(x => x.GetById(cafe.CoffeeChain.Id))
            .ReturnsAsync(coffeeChain);
        _cafeRepositoryFake
            .Setup(x => x.Add(expectedCafe))
            .ReturnsAsync(expectedCafe);

        // Act
        var result = await _cafeService.AddCafe(cafe);

        // Assert
        result.Should().BeEquivalentTo(expectedCafe);

        _coffeeChainServiceFake.Verify(x => x.GetById(cafe.CoffeeChain.Id), Times.Once);
        _cafeRepositoryFake.Verify(x => x.Add(expectedCafe), Times.Once);
    }

    [Fact]
    public async Task AddCafe_WhenCoffeeChainDoesNotExist_ThrowsCoffeeChainNotFoundException()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var expectedException =
            new CoffeeChainNotFoundException($"Coffee chain with id {cafe.CoffeeChain.Id} not found",
                cafe.CoffeeChain.Id);

        _coffeeChainServiceFake
            .Setup(x => x.GetById(cafe.CoffeeChain.Id))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _cafeService.AddCafe(cafe);

        // Assert
        await act.Should().ThrowAsync<CoffeeChainNotFoundException>()
            .WithMessage(expectedException.Message);

        _coffeeChainServiceFake.Verify(x => x.GetById(cafe.CoffeeChain.Id), Times.Once);
        _cafeRepositoryFake.Verify(x => x.Add(It.IsAny<Cafe>()), Times.Never);
    }

    [Fact]
    public async Task UpdateCafe_WhenCafeExists_UpdatesCafe()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate();
        cafeAdmin[0] = cafeAdmin[0].WithCafe(cafe);
        cafe = cafe.WithAdmins(cafeAdmin);

        _cafeRepositoryFake
            .Setup(x => x.GetByAdminId(cafeAdmin[0].Id))
            .ReturnsAsync(cafe);
        _cafeRepositoryFake
            .Setup(x => x.Update(It.IsAny<Cafe>()))
            .Returns(Task.CompletedTask);

        // Act
        await _cafeService.UpdateCafe(cafeAdmin[0].Id, CafeFaker.Generate()[0]);

        // Assert
        _cafeRepositoryFake.Verify(x => x.GetByAdminId(cafeAdmin[0].Id), Times.Once);
        _cafeRepositoryFake.Verify(x => x.Update(It.IsAny<Cafe>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCafe_WhenCafeDoesNotExist_ThrowsCafeNotFoundException()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var cafeAdmin = CafeAdminFaker.Generate();

        _cafeRepositoryFake
            .Setup(x => x.GetByAdminId(cafeAdmin[0].Id))
            .ReturnsAsync(default(Cafe));

        // Act
        var act = async () => await _cafeService.UpdateCafe(cafeAdmin[0].Id, cafe);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage($"Cafe for admin with id: {cafeAdmin[0].Id} not found");

        _cafeRepositoryFake.Verify(x => x.GetByAdminId(cafeAdmin[0].Id), Times.Once);
        _cafeRepositoryFake.Verify(x => x.Update(It.IsAny<Cafe>()), Times.Never);
    }

    [Fact]
    public async Task UpdateCafe_WhenWorkingHoursDoNotExist_ThrowsWorkingHoursNotFoundException()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];
        var week = WorkingHoursFaker.GenerateWeek();
        cafe = cafe.WithWorkingHours(week);
        var updatedCafe = cafe with { WorkingHours = week.Take(2).ToArray() };
        var cafeAdmin = CafeAdminFaker.Generate();
        cafeAdmin[0] = cafeAdmin[0].WithCafe(cafe);
        cafe = cafe.WithAdmins(cafeAdmin);

        _cafeRepositoryFake
            .Setup(x => x.GetByAdminId(cafeAdmin[0].Id))
            .ReturnsAsync(cafe);


        // Act
        var act = async () => await _cafeService.UpdateCafe(cafeAdmin[0].Id, updatedCafe);

        // Assert
        await act.Should().ThrowAsync<WorkingHoursNotFoundException>();

        _cafeRepositoryFake.Verify(x => x.GetByAdminId(cafeAdmin[0].Id), Times.Once);
        _cafeRepositoryFake.Verify(x => x.Update(It.IsAny<Cafe>()), Times.Never);
    }

    [Fact]
    public async Task DeleteCafe_WhenCafeExists_DeletesCafe()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];

        _cafeRepositoryFake
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(cafe);
        _cafeRepositoryFake
            .Setup(x => x.Delete(cafe.Id))
            .Returns(Task.CompletedTask);

        // Act
        await _cafeService.DeleteCafe(cafe.Id);

        // Assert
        _cafeRepositoryFake.Verify(x => x.GetById(cafe.Id), Times.Once);
        _cafeRepositoryFake.Verify(x => x.Delete(cafe.Id), Times.Once);
    }

    [Fact]
    public async Task DeleteCafe_WhenCafeDoesNotExist_ThrowsCafeNotFoundException()
    {
        // Arrange
        var cafe = CafeFaker.Generate()[0];

        _cafeRepositoryFake
            .Setup(x => x.GetById(cafe.Id))
            .ReturnsAsync(default(Cafe));

        // Act
        var act = async () => await _cafeService.DeleteCafe(cafe.Id);

        // Assert
        await act.Should().ThrowAsync<CafeNotFoundException>()
            .WithMessage($"Cafe with id {cafe.Id} not found");

        _cafeRepositoryFake.Verify(x => x.GetById(cafe.Id), Times.Once);
        _cafeRepositoryFake.Verify(x => x.Delete(It.IsAny<long>()), Times.Never);
    }
}