namespace Starter.WebApi.Services.Interfaces;

public interface IUserProfileService
{
    Task<Result<UserProfile>> CreateOrUpdate(UserProfile userProfile);
    Task<Result<UserProfile>> Read();
}