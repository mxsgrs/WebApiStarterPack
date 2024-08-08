namespace Starter.WebApi.Services.Interfaces;

public interface IAuthenticationService
{
    Task<Result<LoginResponse>> CreateJwtBearer(HashedLoginRequest hashedLoginRequest);
}