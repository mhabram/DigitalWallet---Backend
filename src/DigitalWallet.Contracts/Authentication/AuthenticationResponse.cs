namespace DigitalWallet.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string Email,
    string UserName,
    string Token);