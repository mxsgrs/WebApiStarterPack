using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Starter.WebApi.Services;

/// <summary>
/// Access to application context
/// </summary>
/// <param name="httpContext"></param>
/// <param name="actionContextAccessor"></param>
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
                UserCredentialsId = userCredentialsId
            };
        }
    }
}
