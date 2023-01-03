using System.Net;

namespace DigitalWallet.Application.Common.Exceptions.Base;

public interface IBaseException
{
    public HttpStatusCode StatusCode { get; }
    public string Title { get; set; }
}