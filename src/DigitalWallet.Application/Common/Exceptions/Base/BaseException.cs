using DigitalWallet.Domain.Constants;
using System.Net;

namespace DigitalWallet.Application.Common.Exceptions.Base;

public abstract class BaseException : Exception, IBaseException
{
    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public virtual string Title { get; set; } = ErrorMessages.ExceptionTitles.ApplicationException;
}
