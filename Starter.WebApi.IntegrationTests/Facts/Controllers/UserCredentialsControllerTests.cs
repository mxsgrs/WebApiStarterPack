namespace Starter.WebApi.IntegrationTests.Facts.Controllers;

public class UserCredentialsControllerTests(StarterWebApplicationFactory factory)
    : IClassFixture<StarterWebApplicationFactory>
{
    private readonly StarterWebApplicationFactory _factory = factory;

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenUserCredentialsDoNotExist()
    {
        // Arrange
        _factory.MigrateDbContext();
        StarterContext dbContext = _factory.MigrateDbContext();
        ICollection<UserCredentials> userCredentials = [.. dbContext.UserCredentials];
        dbContext.UserCredentials.RemoveRange(userCredentials);
        dbContext.SaveChanges();

        HttpClient client = _factory.CreateClient();
        UserCredentialsDto userCredentialsDto = new()
        {
            EmailAddress = "testuser@gmail.com",
            HashedPassword = "password123",
            UserRole = "admin"
        };

        string json = JsonSerializer.Serialize(userCredentialsDto);
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PostAsync("/api/user-credentials/create-or-update", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenUserCredentialsExist()
    {
        // Arrange
        StarterContext dbContext = _factory.MigrateDbContext();
        UserCredentials userCredentials = new()
        {
            EmailAddress = "testuser@gmail.com",
            HashedPassword = "password123",
            UserRole = "admin"
        };
        dbContext.UserCredentials.Add(userCredentials);
        dbContext.SaveChanges();

        HttpClient client = _factory.CreateAuthorizedClient();
        UserCredentialsDto userCredentialsDto = new()
        {
            EmailAddress = "anothertest@gmail.com",
            HashedPassword = "password123",
            UserRole = "admin"
        };

        string json = JsonSerializer.Serialize(userCredentialsDto);
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PostAsync("/api/user-credentials/create-or-update", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Read_ShouldReturnNotFound_WhenUserCredentialsDoNotExist()
    {
        // Arrange
        StarterContext dbContext = _factory.MigrateDbContext();
        ICollection<UserCredentials> userCredentials = [.. dbContext.UserCredentials];
        dbContext.UserCredentials.RemoveRange(userCredentials);
        dbContext.SaveChanges();

        HttpClient client = _factory.CreateAuthorizedClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/user-credentials/read");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenUserCredentialsExist()
    {
        // Arrange
        StarterContext dbContext = _factory.MigrateDbContext();
        UserCredentials userCredentials = new()
        {
            EmailAddress = "testuser@gmail.com",
            HashedPassword = "password123",
            UserRole = "admin"
        };
        dbContext.UserCredentials.Add(userCredentials);
        dbContext.SaveChanges();

        HttpClient client = _factory.CreateAuthorizedClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/user-credentials/read");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseBody = await response.Content.ReadAsStringAsync();
        UserCredentialsDto? returnedUser = JsonSerializer.Deserialize<UserCredentialsDto>(responseBody, JsonOptions.Default);
        Assert.NotNull(returnedUser);
        Assert.Equal("testuser@gmail.com", returnedUser.EmailAddress);
        Assert.Equal("password123", returnedUser.HashedPassword);
        Assert.Equal("admin", returnedUser.UserRole);
    }
}

