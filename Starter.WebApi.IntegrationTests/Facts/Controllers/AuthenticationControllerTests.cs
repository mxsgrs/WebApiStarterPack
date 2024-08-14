using Starter.WebApi.Models.Authentication;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Starter.WebApi.IntegrationTests.Facts.Controllers;

public class AuthenticationControllerTests
{
    private readonly StarterWebApplicationFactory _factory = new();

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        HashedLoginRequest hashedLoginRequest = new()
        {
            EmailAddress = "testuser@gmail.com",
            HashedPassword = "testpasswordhash"
        };
        string json = JsonSerializer.Serialize(hashedLoginRequest);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpRequestMessage request = new(HttpMethod.Post, "/api/authentication/create-jwt-bearer")
        {
            Content = content
        };

        // Act
        HttpResponseMessage response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        string jsonResponse = await response.Content.ReadAsStringAsync();
        Assert.Contains("token", jsonResponse);
    }

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnUnauthorized_WhenLoginFails()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        HashedLoginRequest hashedLoginRequest = new()
        {
            EmailAddress = "testuser@gmail.com",
            HashedPassword = "testpasswordhash"
        };
        string json = JsonSerializer.Serialize(hashedLoginRequest);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpRequestMessage request = new(HttpMethod.Post, "/api/authentication/create-jwt-bearer")
        {
            Content = content
        };

        // Act
        HttpResponseMessage response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}

