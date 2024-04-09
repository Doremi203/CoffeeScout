namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record AddReviewRequest
(
    string Content,
    int Rating
);