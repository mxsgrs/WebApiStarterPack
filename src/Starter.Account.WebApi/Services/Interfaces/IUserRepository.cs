namespace Starter.Account.WebApi.Services.Interfaces;

public interface IUserRepository
{
    Task<Result<User>> CreateUser(User user);
    Task<Result<User>> GetUser(Guid id);
    Task<Result<User>> GetUser(string emailAddress, string hashedPassword);
    Task<Result<User>> UpdateUser(Guid id, User user);
}