using DigitalWallet.Domain.Constants;
using System.Net;

namespace DigitalWallet.Application.Common.Exceptions.Base;

public abstract class BaseExistsException : Exception, IBaseException
{
    public BaseExistsException(string? exceptionOccurrence)
    {
        Title = exceptionOccurrence + Title;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string Title { get; set; } = ErrorMessages.ExceptionTitles.ExistsException;
}
