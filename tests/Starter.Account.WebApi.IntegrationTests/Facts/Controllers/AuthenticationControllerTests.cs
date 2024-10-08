namespace Starter.Account.WebApi.IntegrationTests.Facts.Controllers;

public class AuthenticationControllerTests(StarterWebApplicationFactory factory)
    : IClassFixture<StarterWebApplicationFactory>
{
    private readonly StarterWebApplicationFactory _factory = factory;

    [Fact]
    public async Task Token_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        AccountContext dbContext = _factory.AccessDbContext();
        if (await dbContext.Users.FindAsync(1L) is null)
        {
            User user = new()
            {
                EmailAddress = "john.doe@example.com",
                HashedPassword = "hashedpassword123",
                FirstName = "John",
                LastName = "Doe",
                Birthday = new DateOnly(1990, 5, 15),
                Gender = Gender.Male,
                Role = Role.Admin,
                Phone = "+1234567890",
                UserAddress = new UserAddress
                {
                    AddressLine = "123 Main St",
                    AddressSupplement = "Apt 4B",
                    City = "Anytown",
                    ZipCode = "12345",
                    StateProvince = "State",
                    Country = "Country"
                }
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        HttpClient client = _factory.CreateClient();
        HashedLoginRequest hashedLoginRequest = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "hashedpassword123"
        };
        string json = JsonSerializer.Serialize(hashedLoginRequest);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpRequestMessage request = new(HttpMethod.Post, "/api/account/authentication/token")
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
}
