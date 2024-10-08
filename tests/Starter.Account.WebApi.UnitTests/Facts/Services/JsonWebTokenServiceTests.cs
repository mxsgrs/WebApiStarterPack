using Starter.Account.WebApi.UnitTests.Facts.Fixtures;

namespace Starter.Account.WebApi.UnitTests.Facts.Services;

public class JsonWebTokenServiceTests : IClassFixture<SharedFixture>
{
    private readonly JsonWebTokenService _jwtService;
    private readonly Mock<ILogger<JsonWebTokenService>> _loggerMock;
    private readonly Mock<IUserRepository> _userServiceMock;

    public JsonWebTokenServiceTests(SharedFixture sharedFixture)
    {
        _loggerMock = new Mock<ILogger<JsonWebTokenService>>();
        _userServiceMock = new Mock<IUserRepository>();
        _jwtService = new JsonWebTokenService(_loggerMock.Object,
            sharedFixture.Configuration, _userServiceMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnFail_WhenLoginIsInvalid()
    {
        // Arrange
        HashedLoginRequest loginRequest = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "wrongpassword"
        };

        _userServiceMock.Setup(x => x.Read(loginRequest.EmailAddress, loginRequest.HashedPassword))
            .ReturnsAsync(Result.Fail<User>("Invalid credentials"));

        // Act
        Result<LoginResponse> result = await _jwtService.Create(loginRequest);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Credentials provided are wrong.", result.Errors.First().Message);
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenLoginIsValid()
    {
        // Arrange
        HashedLoginRequest loginRequest = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "hashedpassword123"
        };
        User user = new()
        {
            Id = 1,
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

        _userServiceMock.Setup(x => x.Read(loginRequest.EmailAddress, loginRequest.HashedPassword))
            .ReturnsAsync(Result.Ok(user));

        // Act
        Result<LoginResponse> result = await _jwtService.Create(loginRequest);

        // Assert
        Assert.True(result.IsSuccess);
        string[] parts = result.Value.AccessToken.Split('.');
        Assert.Equal(3, parts.Length);
    }
}

