using DigitalWallet.Application.Common.Exceptions.Base;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DigitalWallet.Api.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message, errors) = exception switch
        {
            IBaseValidationException serviceValidationException => (
                (int)serviceValidationException.StatusCode,
                serviceValidationException.Title,
                serviceValidationException.Errors),
            IBaseException serviceException => (
                (int)serviceException.StatusCode,
                serviceException.Title,
                null),
            _ => (StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                null),
        };

        return errors == null
                ? Problem(
                    statusCode: statusCode,
                    title: message)
                : ValidationProblem(
                    modelStateDictionary: CreateModelStateDictionary(errors),
                    statusCode: statusCode,
                    title: message);
    }

    private static ModelStateDictionary CreateModelStateDictionary(IDictionary<string, string[]> errors)
    {
        ModelStateDictionary modelStateDictionary = new();

        foreach (var errorArray in errors.Values)
        {
            foreach (var error in errorArray)
            {
                modelStateDictionary.AddModelError(errors.Keys.First(), error);
            }
        }

        return modelStateDictionary;
    }
}