using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using AuthenticationService = WebDisassembler.Core.Application.Services.AuthenticationService;

namespace WebDisassembler.Api.Authentication;

public class TokenAuthenticationSchemeHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string Cookie = "AuthToken";
    public const string IdClaim = "Id";

    private readonly IMemoryCache _memoryCache;
    private readonly AuthenticationService _authenticationService;

    public TokenAuthenticationSchemeHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IMemoryCache memoryCache, AuthenticationService authenticationService) : base(options, logger, encoder, clock)
    {
        _memoryCache = memoryCache;
        _authenticationService = authenticationService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Context.Request.Cookies.TryGetValue(Cookie, out var token))
        {
            if (token.Length != 128)
            {
                return AuthenticateResult.Fail("Invalid token format");
            }

            Guid userId;
            if (_memoryCache.TryGetValue($"login_{token}", out Guid cachedUserId))
            {
                userId = cachedUserId;
            }
            else
            {
                var user = await _authenticationService.GetUserFromToken(token);
                if (user == null)
                {
                    return AuthenticateResult.Fail("Invalid token");
                }

                _memoryCache.Set($"login_{token}", user.Id);
                userId = user.Id;
            }
            
            var claims = new[]
            {
                new Claim(IdClaim, userId.ToString())
            };

            return AuthenticateResult.Success(
                new AuthenticationTicket(
                    new ClaimsPrincipal(
                        new ClaimsIdentity(
                            claims,
                            "Token"
                        )
                    ),
                    Scheme.Name
                )
            );
        }

        return AuthenticateResult.NoResult();
    }
}