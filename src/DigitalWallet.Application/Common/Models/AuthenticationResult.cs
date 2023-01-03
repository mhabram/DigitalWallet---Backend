namespace DigitalWallet.Application.Common.Models;

public record AuthenticationResult(
    Guid Id,
    string Email,
    string UserName,
    string Token);