namespace Starter.Account.WebApi.Services.Interfaces;

public interface IJsonWebTokenService
{
    Task<Result<LoginResponse>> CreateToken(HashedLoginRequest hashedLoginRequest);
}