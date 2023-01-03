using DigitalWallet.Application.Common.Exceptions.Base;

namespace DigitalWallet.Application.Common.Exceptions.Common;

public class ObjectExistsException : BaseExistsException
{
    public ObjectExistsException(string? exceptionOccurrence) : base(exceptionOccurrence)
    {
    }
}
