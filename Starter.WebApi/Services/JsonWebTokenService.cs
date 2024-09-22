using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Starter.WebApi.Services;

public class JsonWebTokenService(ILogger<JsonWebTokenService> logger, IConfiguration configuration,
    IUserRepository userService) : IJsonWebTokenService
{
    private readonly ILogger<JsonWebTokenService> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _userService = userService;

    /// <summary>
    /// Generate a JSON based on the request
    /// </summary>
    public async Task<Result<LoginResponse>> Create(HashedLoginRequest hashedLoginRequest)
    {
        _logger.LogDebug("Hashed login request is {HashedLoginRequest}", hashedLoginRequest);

        Result<long> result = await ValidateUser(hashedLoginRequest);

        if (result.IsFailed)
        {
            return Result.Fail("Credentials provided are wrong.");
        }

        JsonWebTokenParameters jwtParameters = _configuration
            .GetRequiredSection("Jwt").Get<JsonWebTokenParameters>()
                ?? throw new Exception("JWT settings are not configured");

        byte[] encodedKey = Encoding.ASCII.GetBytes(jwtParameters.Key);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, result.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, hashedLoginRequest.EmailAddress)
            ]),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = jwtParameters.Issuer,
            Audience = jwtParameters.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(encodedKey),
                SecurityAlgorithms.HmacSha512Signature)
        };

        JsonWebTokenHandler handler = new()
        {
            SetDefaultTimesOnTokenCreation = false
        };

        string accessToken = handler.CreateToken(tokenDescriptor);

        LoginResponse loginResponse = new()
        {
            AccessToken = accessToken
        };

        return Result.Ok(loginResponse);
    }

    /// <summary>
    /// Verify the user exists in the database
    /// </summary>
    private async Task<Result<long>> ValidateUser(HashedLoginRequest hashedLoginRequest)
    {
        Result<User> result = await _userService.Read(hashedLoginRequest.EmailAddress,
            hashedLoginRequest.HashedPassword);

        if (result.IsFailed)
        {
            IError? error = result.Errors.FirstOrDefault();
            return Result.Fail(error);
        }

        return Result.Ok(result.Value.Id);
    }
}
