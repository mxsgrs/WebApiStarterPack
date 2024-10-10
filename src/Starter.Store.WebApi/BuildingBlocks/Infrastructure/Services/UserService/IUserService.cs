
namespace Starter.Store.WebApi.BuildingBlocks.Infrastructure.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserDto> ReadUserAsync(Guid id);
    }
}