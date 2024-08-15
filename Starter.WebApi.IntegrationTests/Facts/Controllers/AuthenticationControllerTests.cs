namespace Starter.WebApi.IntegrationTests.Facts.Controllers;

public class AuthenticationControllerTests(StarterWebApplicationFactory factory) 
    : IClassFixture<StarterWebApplicationFactory>
{
    private readonly StarterWebApplicationFactory _factory = factory;

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        StarterContext dbContext = _factory.MigrateDbContext();
        UserCredentials userCredentials = new()
        {
            EmailAddress = "testuser@gmail.com",
            HashedPassword = "testpasswordhash",
            UserRole = "admin"
        };
        dbContext.UserCredentials.Add(userCredentials);
        dbContext.SaveChanges();

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
        Assert.Contains("accessToken", jsonResponse);
    }

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnUnauthorized_WhenLoginFails()
    {
        // Arrange
        _factory.MigrateDbContext();
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
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
