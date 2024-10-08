namespace Starter.Account.WebApi.Services.Interfaces;

public interface IUserRepository
{
    Task<Result<User>> CreateOrUpdate(User user);
    Task<Result<User>> Read();
    Task<Result<User>> Read(string emailAddress, string hashedPassword);
}