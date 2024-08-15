namespace Starter.WebApi.UnitTests.Facts.Services;

public class AuthenticationServiceTests(SharedFixture fixture) : IClassFixture<SharedFixture>
{
    private readonly SharedFixture _fixture = fixture;

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnSuccess_WhenCredentialsAreValid()
    {
        // Arrange
        Mock<ILogger<AuthenticationService>> loggerMock = new();
        Mock<IUserCredentialsService> userCredentialsServiceMock = new();
        HashedLoginRequest hashedLoginRequest = new()
        {
            EmailAddress = "test@example.com",
            HashedPassword = "hashedpassword"
        };
        UserCredentials userCredentials = new()
        {
            Id = 1,
            EmailAddress = "test@example.com",
            HashedPassword = "hashedpassword"
        };
        userCredentialsServiceMock.Setup(s => s.Read(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result.Ok(userCredentials));

        AuthenticationService authenticationService = new(loggerMock.Object,
            _fixture.Configuration, userCredentialsServiceMock.Object);

        // Act
        Result<LoginResponse> result = await authenticationService.CreateJwtBearer(hashedLoginRequest);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotEmpty(result.Value.AccessToken);
    }

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnFailure_WhenCredentialsAreInvalid()
    {
        // Arrange
        Mock<ILogger<AuthenticationService>> loggerMock = new();
        Mock<IUserCredentialsService> userCredentialsServiceMock = new();
        HashedLoginRequest hashedLoginRequest = new()
        {
            EmailAddress = "test@example.com",
            HashedPassword = "invalidpassword"
        };
        userCredentialsServiceMock.Setup(s => s.Read(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result.Fail<UserCredentials>("Invalid credentials"));

        AuthenticationService authenticationService = new(loggerMock.Object,
            _fixture.Configuration, userCredentialsServiceMock.Object);

        // Act
        Result<LoginResponse> result = await authenticationService.CreateJwtBearer(hashedLoginRequest);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Credentials provided are wrong.", result.Errors.First().Message);
    }
}
