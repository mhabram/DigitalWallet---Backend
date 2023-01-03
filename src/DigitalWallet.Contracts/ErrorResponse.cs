namespace DigitalWallet.Contracts;

public record ErrorResponse(
    string? Type,
    string? Title,
    int? ErrorCode,
    object[]? Errors);