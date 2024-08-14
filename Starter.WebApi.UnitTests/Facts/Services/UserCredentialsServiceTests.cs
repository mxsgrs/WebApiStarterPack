namespace Starter.WebApi.UnitTests.Facts.Services;

public class UserCredentialsServiceTests(SharedFixture fixture) : IClassFixture<SharedFixture>
{
    private readonly SharedFixture _fixture = fixture;
    private readonly Mock<ILogger<UserCredentialsService>> _loggerMock = new();

    [Fact]
    public async Task CreateOrUpdate_ShouldCreateNewUserCredentials_WhenUserCredentialsDoesNotExist()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserCredentialsService service = new(
            _loggerMock.Object,
            dbContext,
            _fixture.AppContextAccessor);
        UserCredentials newUserCredentials = new()
        {
            Id = 2,
            EmailAddress = "newuser@example.com",
            HashedPassword = "hashedpassword",
            UserRole = "User"
        };

        // Act
        Result<UserCredentials> result = await service.CreateOrUpdate(newUserCredentials);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.EmailAddress.Should().Be("newuser@example.com");
        dbContext.UserCredentials.Should().Contain(u => u.EmailAddress == "newuser@example.com");
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldUpdateExistingUserCredentials_WhenUserCredentialsExist()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserCredentials existingUserCredentials = new()
        {
            Id = 1,
            EmailAddress = "existinguser@example.com",
            HashedPassword = "oldpassword",
            UserRole = "Admin"
        };
        dbContext.UserCredentials.Add(existingUserCredentials);
        dbContext.SaveChanges();

        UserCredentialsService service = new(
            _loggerMock.Object,
            dbContext,
            _fixture.AppContextAccessor);
        UserCredentials updatedUserCredentials = new()
        {
            Id = 1,
            EmailAddress = "updateduser@example.com",
            HashedPassword = "newpassword",
            UserRole = "Admin"
        };

        // Act
        Result<UserCredentials> result = await service.CreateOrUpdate(updatedUserCredentials);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.EmailAddress.Should().Be("updateduser@example.com");
        dbContext.UserCredentials.Should().Contain(u => u.EmailAddress == "updateduser@example.com");
    }

    [Fact]
    public async Task Read_ShouldReturnUserCredentials_WhenUserCredentialsExist()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserCredentials existingUserCredentials = new()
        {
            Id = 1,
            EmailAddress = "existinguser@example.com",
            HashedPassword = "password",
            UserRole = "User"
        };
        dbContext.UserCredentials.Add(existingUserCredentials);
        dbContext.SaveChanges();

        UserCredentialsService service = new(
            _loggerMock.Object,
            dbContext,
            _fixture.AppContextAccessor);

        // Act
        Result<UserCredentials> result = await service.Read();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.EmailAddress.Should().Be("existinguser@example.com");
    }

    [Fact]
    public async Task Read_ShouldReturnFailure_WhenUserCredentialsDoNotExist()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserCredentialsService service = new(
            _loggerMock.Object,
            dbContext,
            _fixture.AppContextAccessor);

        // Act
        Result<UserCredentials> result = await service.Read();

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Message.Should().Contain("User credentials does not exist.");
    }

    [Fact]
    public async Task Read_ShouldReturnUserCredentials_WhenEmailAndPasswordMatch()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserCredentials existingUserCredentials = new()
        {
            Id = 1,
            EmailAddress = "existinguser@example.com",
            HashedPassword = "password",
            UserRole = "User"
        };
        dbContext.UserCredentials.Add(existingUserCredentials);
        dbContext.SaveChanges();

        UserCredentialsService service = new(
            _loggerMock.Object,
            dbContext,
            _fixture.AppContextAccessor);

        // Act
        Result<UserCredentials> result = await service.Read("existinguser@example.com", "password");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.EmailAddress.Should().Be("existinguser@example.com");
    }

    [Fact]
    public async Task Read_ShouldReturnFailure_WhenEmailAndPasswordDoNotMatch()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserCredentialsService service = new(
            _loggerMock.Object,
            dbContext,
            _fixture.AppContextAccessor);

        // Act
        Result<UserCredentials> result = await service.Read("nonexistentuser@example.com", "wrongpassword");

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Message.Should().Contain("User credentials does not exist.");
    }
}
