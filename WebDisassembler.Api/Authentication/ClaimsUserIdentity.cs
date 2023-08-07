using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Authentication;

public class ClaimsUserIdentity : IUserIdentity
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ClaimsUserIdentity(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public bool IsLoggedIn
    {
        get => _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
    
    public Guid UserId { get => GetUserId(); }

    private Guid GetUserId()
    {
        var context = _contextAccessor.HttpContext;
        if (context == null)
        {
            return default;
        }

        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == TokenAuthenticationSchemeHandler.IdClaim);
        if (userIdClaim == null)
        {
            return default;
        }

        return Guid.Parse(userIdClaim.Value);
    }
}