namespace Starter.Store.WebApi.BuildingBlocks.Infrastructure.Services.UserService;

public class UserService(HttpClient httpClient) : IUserService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", userDto);

        response.EnsureSuccessStatusCode(); // Throws if not a success code.

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    public async Task<UserDto> ReadUserAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/users/{id}");

        response.EnsureSuccessStatusCode(); // Throws if not a success code.

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
}
