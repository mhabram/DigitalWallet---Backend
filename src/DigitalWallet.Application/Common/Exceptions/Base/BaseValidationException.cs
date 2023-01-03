using DigitalWallet.Domain.Constants;
using System.Net;

namespace DigitalWallet.Application.Common.Exceptions.Base;

public abstract class BaseValidationException : Exception, IBaseValidationException
{
    public BaseValidationException(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string Title { get; set; } = ErrorMessages.ExceptionTitles.ValidationException;
}
