using DigitalWallet.Application.Common.Exceptions.Base;

namespace DigitalWallet.Application.Common.Exceptions;

public class DigitalWalletValidationException : BaseValidationException
{
    public DigitalWalletValidationException(IDictionary<string, string[]> errors) : base(errors)
    {
    }
}