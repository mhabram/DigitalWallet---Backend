namespace DigitalWallet.Contracts.Authentication;

public record SignUpRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string CountryCode,
    string PhoneNumber,
    string Password,
    string ConfirmPassword);