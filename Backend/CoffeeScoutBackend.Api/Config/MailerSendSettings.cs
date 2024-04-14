namespace CoffeeScoutBackend.Api.Config;

public record MailerSendSettings
{
    public required string ApiUrl { get; init; }
    public required string ApiToken { get; init; }
    public required bool UseRetryPolicy { get; init; }
    public required int RetryCount { get; init; }
}