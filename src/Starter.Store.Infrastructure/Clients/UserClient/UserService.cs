using System.Net.Http.Json;

namespace Starter.Store.Infrastructure.Clients.UserClient;

public class UserService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/account/user", userDto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    public async Task<UserDto> ReadUserAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/account/user/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
}
