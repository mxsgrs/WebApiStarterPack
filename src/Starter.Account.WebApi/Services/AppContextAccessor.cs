using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Starter.Account.WebApi.Services;

public class AppContextAccessor(IHttpContextAccessor httpContext) : IAppContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

    public UserClaims UserClaims
    {
        get
        {
            string? sub = _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            _ = Guid.TryParse(sub, out Guid id);

            return new()
            {
                Id = id
            };
        }
    }
}
