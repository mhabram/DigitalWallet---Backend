namespace DigitalWallet.Domain.Constants;

public static class ErrorMessages
{
    public static class ExceptionTitles
    {
        public const string ApplicationException = "ApplicationException";
        public const string ExistsException = "ExistsException";
        public const string ValidationException = "ValidationException";

        public const string SignUpCommand = "SignUpCommandException";
        public const string SignInCommand = "SignInCommandException";
    }

    public static class ExceptionMessages
    {
        public const string EmailUnique = "The specified email already exist";
        public const string PhoneNumberDoesNotMatch = "Phone number does not match";
        public const string PasswordDoesNotMatch = "Password does not match";
    }
}