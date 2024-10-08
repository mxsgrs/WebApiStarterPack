namespace Starter.Account.WebApi.Services.Interfaces;

public interface IJsonWebTokenService
{
    Task<Result<LoginResponse>> Create(HashedLoginRequest hashedLoginRequest);
}