namespace Starter.WebApi.Services.Interfaces;

public interface IUserCredentialsService
{
    Task<Result<UserCredentials>> CreateOrUpdate(UserCredentials newUserCredentials);
    Task<Result<UserCredentials>> Read();
    Task<Result<UserCredentials>> Read(string emailAddress, string hashedPassword);
}