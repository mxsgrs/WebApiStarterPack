namespace Starter.WebApi.IntegrationTests.Facts.Controllers;

public class AuthenticationControllerTests
{
    [Fact]
    public async Task CreateJwtBearer_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        static void AddUserCredentials(StarterContext dbContext)
        {
            UserCredentials userCredentials = new()
            {
                EmailAddress = "testuser@gmail.com",
                HashedPassword = "testpasswordhash",
                UserRole = "admin"
            };
            dbContext.UserCredentials.Add(userCredentials);
            dbContext.SaveChanges();
        }
        StarterWebApplicationFactory factory = new(AddUserCredentials);
        HttpClient client = factory.CreateClient();
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
        Assert.Contains("accessToken", jsonResponse);
    }

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnUnauthorized_WhenLoginFails()
    {
        // Arrange
        StarterWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();
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
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
