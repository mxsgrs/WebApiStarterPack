using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Starter.WebApi.Services;

public class AppContextAccessor(IHttpContextAccessor httpContext) : IAppContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

    /// <summary>
    /// Provide user claims
    /// </summary>
    public UserClaims UserClaims
    {
        get
        {
            _ = long.TryParse(_httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub), out long userCredentialsId);

            return new()
            {
                Id = userCredentialsId
            };
        }
    }
}
