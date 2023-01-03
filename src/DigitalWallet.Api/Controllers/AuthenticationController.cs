using DigitalWallet.Application.Authentication.Commands.SignIn;
using DigitalWallet.Application.Authentication.Commands.SignUp;
using DigitalWallet.Application.Common.Models;
using DigitalWallet.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Api.Controllers;

[AllowAnonymous]
public class AuthenticationController : ApiControllerBase
{
    [HttpPost("[Action]")]
    public async Task<ActionResult<AuthenticationResponse>> SignUp([FromBody] SignUpRequest request)
    {
        var command = new SignUpCommand(
            request.UserName,
            request.FirstName,
            request.LastName,
            request.Email,
            request.CountryCode,
            request.PhoneNumber,
            request.Password,
            request.ConfirmPassword);

        var result = await Mediator.Send(command);
        
        return Ok(MapToAuthenticationResponse(result));
    }

    [HttpPost("[Action]")]
    public async Task<ActionResult<AuthenticationResponse>> SignIn([FromBody] SignInRequest request)
    {
        var command = new SignInCommand(
            request.Email,
            request.Password);

        var result = await Mediator.Send(command);

        return Ok(MapToAuthenticationResponse(result));
    }

    private static AuthenticationResponse MapToAuthenticationResponse(AuthenticationResult result)
    {
        return new AuthenticationResponse(
                    result.Id,
                    result.Email,
                    result.UserName,
                    result.Token);
    }

}