using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.UnitTests.Fakers;
using FluentAssertions;
using Moq;

namespace CoffeeScoutBackend.UnitTests.Tests;

public class ReviewServiceTests
{
    private readonly Mock<IMenuItemService> _menuItemServiceFake = new(MockBehavior.Strict);
    private readonly Mock<ICustomerService> _customerServiceFake = new(MockBehavior.Strict);
    private readonly Mock<IReviewRepository> _reviewRepositoryFake = new(MockBehavior.Strict);
    private readonly IReviewService _reviewService;

    public ReviewServiceTests()
    {
        _reviewService = new ReviewService(
            _menuItemServiceFake.Object,
            _customerServiceFake.Object,
            _reviewRepositoryFake.Object
        );
    }

    [Fact]
    public async Task Add_WithExistingMenuItem_ReturnsCreatedReview()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var customer = CustomerFaker.Generate()[0];
        var review = ReviewFaker.Generate()[0]
            .WithCustomer(customer)
            .WithMenuItem(menuItem);

        _menuItemServiceFake
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(menuItem);

        _customerServiceFake
            .Setup(x => x.GetByUserId(customer.Id))
            .ReturnsAsync(customer);

        _reviewRepositoryFake
            .Setup(x => x.AddReview(It.IsAny<Review>()))
            .ReturnsAsync(review);

        // Act
        var result = await _reviewService.Add(menuItem.Id, customer.Id, review);

        // Assert
        result.Should().BeEquivalentTo(review);
        
        _menuItemServiceFake.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _customerServiceFake.Verify(x => x.GetByUserId(customer.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.AddReview(It.IsAny<Review>()), Times.Once);
    }
    
    [Fact]
    public async Task Add_WithNonExistingMenuItem_ThrowsMenuItemNotFoundException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var customer = CustomerFaker.Generate()[0];
        var review = ReviewFaker.Generate()[0]
            .WithCustomer(customer)
            .WithMenuItem(menuItem);
        var expectedException = new MenuItemNotFoundException(
            $"MenuItem with id: {menuItem.Id} not found", menuItem.Id);

        _menuItemServiceFake
            .Setup(x => x.GetById(menuItem.Id))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _reviewService.Add(menuItem.Id, customer.Id, review);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage(expectedException.Message);

        _menuItemServiceFake.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _customerServiceFake.Verify(x => x.GetByUserId(customer.Id), Times.Never);
        _reviewRepositoryFake.Verify(x => x.AddReview(It.IsAny<Review>()), Times.Never);
    }
    
    [Fact]
    public async Task Add_WithNonExistingCustomer_ThrowsCustomerNotFoundException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var customer = CustomerFaker.Generate()[0];
        var review = ReviewFaker.Generate()[0]
            .WithCustomer(customer)
            .WithMenuItem(menuItem);
        var expectedException = new CustomerNotFoundException(
            $"Customer with id: {customer.Id} not found", customer.Id);

        _menuItemServiceFake
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(menuItem);

        _customerServiceFake
            .Setup(x => x.GetByUserId(customer.Id))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _reviewService.Add(menuItem.Id, customer.Id, review);

        // Assert
        await act.Should().ThrowAsync<CustomerNotFoundException>()
            .WithMessage(expectedException.Message);

        _menuItemServiceFake.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _customerServiceFake.Verify(x => x.GetByUserId(customer.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.AddReview(It.IsAny<Review>()), Times.Never);
    }
    
    [Fact]
    public async Task GetById_WithExistingReview_ReturnsReview()
    {
        // Arrange
        var review = ReviewFaker.Generate()[0];
        
        _reviewRepositoryFake
            .Setup(x => x.GetById(review.Id))
            .ReturnsAsync(review);

        // Act
        var result = await _reviewService.GetById(review.Id);

        // Assert
        result.Should().BeEquivalentTo(review);
        
        _reviewRepositoryFake.Verify(x => x.GetById(review.Id), Times.Once);
    }
    
    [Fact]
    public async Task GetById_WithNonExistingReview_ThrowsReviewNotFoundException()
    {
        // Arrange
        var review = ReviewFaker.Generate()[0];
        var expectedException = new ReviewNotFoundException(
            $"Review with id: {review.Id} not found", review.Id);
        
        _reviewRepositoryFake
            .Setup(x => x.GetById(review.Id))
            .ReturnsAsync(default(Review));

        // Act
        Func<Task> act = async () => await _reviewService.GetById(review.Id);

        // Assert
        await act.Should().ThrowAsync<ReviewNotFoundException>()
            .WithMessage(expectedException.Message);
        
        _reviewRepositoryFake.Verify(x => x.GetById(review.Id), Times.Once);
    }
    
    [Fact]
    public async Task GetByMenuItemId_WithExistingMenuItem_ReturnsReviews()
    {
        // Arrange
        var reviews = ReviewFaker.Generate(3);
        var menuItem = MenuItemFaker.Generate()[0]
            .WithReviews(reviews);
        
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItem.Id))
            .ReturnsAsync(menuItem);
        
        _reviewRepositoryFake
            .Setup(x => x.GetByMenuItemId(menuItem.Id))
            .ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetByMenuItemId(menuItem.Id);

        // Assert
        result.Should().BeEquivalentTo(reviews);
        
        _menuItemServiceFake.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.GetByMenuItemId(menuItem.Id), Times.Once);
    }
    
    [Fact]
    public async Task GetByMenuItemId_WithNonExistingMenuItem_ThrowsMenuItemNotFoundException()
    {
        // Arrange
        var menuItem = MenuItemFaker.Generate()[0];
        var expectedException = new MenuItemNotFoundException(
            $"MenuItem with id: {menuItem.Id} not found", menuItem.Id);
        
        _menuItemServiceFake
            .Setup(x => x.GetById(menuItem.Id))
            .ThrowsAsync(expectedException);

        // Act
        Func<Task> act = async () => await _reviewService.GetByMenuItemId(menuItem.Id);

        // Assert
        await act.Should().ThrowAsync<MenuItemNotFoundException>()
            .WithMessage(expectedException.Message);
        
        _menuItemServiceFake.Verify(x => x.GetById(menuItem.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.GetByMenuItemId(menuItem.Id), Times.Never);
    }
    
    [Fact]
    public async Task UpdateReview_WithExistingReview_UpdatesReview()
    {
        // Arrange
        var review = ReviewFaker.Generate()[0];
        
        _reviewRepositoryFake
            .Setup(x => x.GetById(review.Id))
            .ReturnsAsync(review);

        _reviewRepositoryFake
            .Setup(x => x.UpdateReview(It.IsAny<Review>()))
            .Returns(Task.CompletedTask);

        // Act
        await _reviewService.UpdateReview(review.Id, review);

        // Assert
        _reviewRepositoryFake.Verify(x => x.GetById(review.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.UpdateReview(It.IsAny<Review>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateReview_WithNonExistingReview_ThrowsReviewNotFoundException()
    {
        // Arrange
        var review = ReviewFaker.Generate()[0];
        var expectedException = new ReviewNotFoundException(
            $"Review with id: {review.Id} not found", review.Id);
        
        _reviewRepositoryFake
            .Setup(x => x.GetById(review.Id))
            .ReturnsAsync(default(Review));

        // Act
        Func<Task> act = async () => await _reviewService.UpdateReview(review.Id, review);

        // Assert
        await act.Should().ThrowAsync<ReviewNotFoundException>()
            .WithMessage(expectedException.Message);
        
        _reviewRepositoryFake.Verify(x => x.GetById(review.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.UpdateReview(It.IsAny<Review>()), Times.Never);
    }
    
    [Fact]
    public async Task Delete_WithExistingReview_DeletesReview()
    {
        // Arrange
        var review = ReviewFaker.Generate()[0];
        
        _reviewRepositoryFake
            .Setup(x => x.GetById(review.Id))
            .ReturnsAsync(review);

        _reviewRepositoryFake
            .Setup(x => x.DeleteReview(review.Id))
            .Returns(Task.CompletedTask);

        // Act
        await _reviewService.Delete(review.Id);

        // Assert
        _reviewRepositoryFake.Verify(x => x.GetById(review.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.DeleteReview(review.Id), Times.Once);
    }
    
    [Fact]
    public async Task Delete_WithNonExistingReview_ThrowsReviewNotFoundException()
    {
        // Arrange
        var review = ReviewFaker.Generate()[0];
        var expectedException = new ReviewNotFoundException(
            $"Review with id: {review.Id} not found", review.Id);
        
        _reviewRepositoryFake
            .Setup(x => x.GetById(review.Id))
            .ReturnsAsync(default(Review));

        // Act
        Func<Task> act = async () => await _reviewService.Delete(review.Id);

        // Assert
        await act.Should().ThrowAsync<ReviewNotFoundException>()
            .WithMessage(expectedException.Message);
        
        _reviewRepositoryFake.Verify(x => x.GetById(review.Id), Times.Once);
        _reviewRepositoryFake.Verify(x => x.DeleteReview(review.Id), Times.Never);
    }
}