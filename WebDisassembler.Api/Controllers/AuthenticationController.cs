using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Api.Authentication;
using WebDisassembler.Core.Application.Models.Authentication;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;
    private readonly IUserIdentity _userIdentity;

    public AuthenticationController(AuthenticationService authenticationService, IUserIdentity userIdentity)
    {
        _authenticationService = authenticationService;
        _userIdentity = userIdentity;
    }

    [HttpPost("login"), SwaggerOperation(OperationId = nameof(Login)), AllowAnonymous]
    public async ValueTask<ActionResult> Login(LoginRequest request)
    {
        var token = await _authenticationService.Login(request);
        Response.Cookies.Append(TokenAuthenticationSchemeHandler.Cookie, token);
        return Ok();
    }

    [HttpGet("me"), SwaggerOperation(OperationId = nameof(Me))]
    public async ValueTask<CurrentUser> Me()
    {
        return await _authenticationService.GetCurrentUser(_userIdentity.UserId);
    }
}