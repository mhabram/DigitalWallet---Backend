using DigitalWallet.Application.Common.Exceptions.Base;
using DigitalWallet.Domain.Constants;

namespace DigitalWallet.Application.Common.Exceptions.Authentication;

public class SignInCommandException : BaseException
{
    public override string Title
    { 
        get => base.Title;
        set => base.Title = ErrorMessages.ExceptionTitles.SignInCommand;
    }
}
