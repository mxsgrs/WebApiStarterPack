using Starter.WebApi.Models.DataTransferObjects;

namespace Starter.WebApi.IntegrationTests.Facts.Controllers;

public class AuthenticationControllerTests(StarterWebApplicationFactory factory) 
    : IClassFixture<StarterWebApplicationFactory>
{
    private readonly StarterWebApplicationFactory _factory = factory;

    [Fact]
    public async Task GenerateToken_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        StarterContext dbContext = _factory.AccessDbContext();
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
        HttpRequestMessage request = new(HttpMethod.Post, "/api/authentication/generate-token")
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
    public async Task Profile_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
        StarterContext dbContext = _factory.AccessDbContext();
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

        HttpClient client = _factory.CreateAuthorizedClient();
        HttpRequestMessage request = new(HttpMethod.Get, "/api/authentication/profile");

        // Act
        HttpResponseMessage response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenUserIsValid()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        UserDto userDto = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "hashedpassword123",
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1990, 5, 15),
            Gender = Gender.Male,
            Role = Role.Admin,
            Phone = "+1234567890",
            UserAddress = new UserAddressDto
            {
                AddressLine = "123 Main St",
                AddressSupplement = "Apt 4B",
                City = "Anytown",
                ZipCode = "12345",
                StateProvince = "State",
                Country = "Country"
            }
        };
        string json = JsonSerializer.Serialize(userDto);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpRequestMessage request = new(HttpMethod.Post, "/api/authentication/register")
        {
            Content = content
        };

        // Act
        HttpResponseMessage response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
