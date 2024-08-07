using FluentResults;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Starter.WebApi.Services;

/// <summary>
/// Authentication operations
/// </summary>
/// <param name="logger">Logging interface</param>
/// <param name="configuration">Application configuration</param>
/// <param name="userCredentialsService">User credentials CRUD operations</param>
public class AuthenticationService(ILogger<AuthenticationService> logger, IConfiguration configuration,
    IUserCredentialsService userCredentialsService) : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserCredentialsService _userCredentialsService = userCredentialsService;

    /// <summary>
    /// Create a token based on user login
    /// </summary>
    /// <param name="hashedLoginRequest"></param>
    /// <return>User token</returns>
    public async Task<Result<LoginResponse>> CreateJwtBearer(HashedLoginRequest hashedLoginRequest)
    {
        _logger.LogDebug("Hashed login request is {HashedLoginRequest}", hashedLoginRequest);

        Result<long> result = await ValidateUser(hashedLoginRequest);

        if (result.IsFailed)
        {
            return Result.Fail("Credentials provided are wrong.");
        }

        string issuer = _configuration["Jwt:Issuer"]
            ?? throw new Exception("Jwt:Issuer is not set");

        string audience = _configuration["Jwt:Audience"]
            ?? throw new Exception("Jwt:Audience is not set");

        string key = _configuration["Jwt:Key"]
            ?? throw new Exception("Jwt:Key is not set");

        byte[] encodedKey = Encoding.ASCII.GetBytes(key);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, result.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, hashedLoginRequest.EmailAddress)
            ]),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
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
    /// Check user credentials
    /// </summary>
    /// <param name="hashedLoginRequest"></param>
    /// <returns>User identifier</returns>
    private async Task<Result<long>> ValidateUser(HashedLoginRequest hashedLoginRequest)
    {
        Result<UserCredentials> result = await _userCredentialsService.Read(hashedLoginRequest.EmailAddress,
            hashedLoginRequest.HashedPassword);

        if (result.IsFailed)
        {
            IError? error = result.Errors.FirstOrDefault();
            return Result.Fail(error);
        }

        return Result.Ok(result.Value.Id);
    }
}
