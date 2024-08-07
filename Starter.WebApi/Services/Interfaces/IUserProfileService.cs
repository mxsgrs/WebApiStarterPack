namespace Starter.WebApi.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfile?> CreateOrUpdate(UserProfile userProfile);
        Task<UserProfile?> Read();
    }
}