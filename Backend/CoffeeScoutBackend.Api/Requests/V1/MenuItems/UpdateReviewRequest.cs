namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record UpdateReviewRequest(
    int Rating,
    string Content
);