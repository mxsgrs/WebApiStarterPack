using Starter.WebApi.UnitTests.Facts.Fixtures;

namespace Starter.WebApi.UnitTests.Facts.Services;

public class UserProfileServiceTests(SharedFixture fixture) : IClassFixture<SharedFixture>
{
    private readonly SharedFixture _fixture = fixture;
    private readonly Mock<ILogger<UserProfileService>> _logger = new();

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenNewUserProfileIsCreated()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserProfileService service = new(_logger.Object, dbContext, _fixture.AppContextAccessor);
        UserProfile newUserProfile = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1990, 1, 1),
            Gender = "Male",
            PersonalPhone = "+1234567890",
            PostalAddress = "123 Main St",
            City = "Anytown",
            ZipCode = "12345",
            Country = "USA"
        };

        // Act
        Result<UserProfile> result = await service.CreateOrUpdate(newUserProfile);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(newUserProfile.FirstName, result.Value.FirstName);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenExistingUserProfileIsUpdated()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserProfile existingProfile = new()
        {
            UserCredentialsId = _fixture.AppContextAccessor.UserClaims.UserCredentialsId,
            FirstName = "Jane",
            LastName = "Doe",
            Birthday = new DateOnly(1992, 2, 2),
            Gender = "Female",
            PersonalPhone = "+1234567891",
            PostalAddress = "456 Main St",
            City = "Othertown",
            ZipCode = "67890",
            Country = "Canada"
        };
        dbContext.UserProfile.Add(existingProfile);
        dbContext.SaveChanges();

        UserProfile updatedProfile = new()
        {
            FirstName = "Jane Updated",
            LastName = "Doe Updated",
            Birthday = new DateOnly(1992, 2, 2),
            Gender = "Female",
            PersonalPhone = "+1234567891",
            PostalAddress = "456 Main St",
            City = "Newtown",
            ZipCode = "67890",
            Country = "Canada"
        };

        UserProfileService service = new(_logger.Object, dbContext, _fixture.AppContextAccessor);

        // Act
        Result<UserProfile> result = await service.CreateOrUpdate(updatedProfile);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Jane Updated", result.Value.FirstName);
        Assert.Equal("Doe Updated", result.Value.LastName);
        Assert.Equal("Newtown", result.Value.City);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenUserProfileExists()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserProfile existingProfile = new()
        {
            UserCredentialsId = _fixture.AppContextAccessor.UserClaims.UserCredentialsId,
            FirstName = "John",
            LastName = "Smith",
            Birthday = new DateOnly(1985, 5, 5),
            Gender = "Male",
            PersonalPhone = "+9876543210",
            PostalAddress = "789 Elm St",
            City = "Bigcity",
            ZipCode = "54321",
            Country = "USA"
        };
        dbContext.UserProfile.Add(existingProfile);
        dbContext.SaveChanges();

        UserProfileService service = new(_logger.Object, dbContext, _fixture.AppContextAccessor);

        // Act
        Result<UserProfile> result = await service.Read();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("John", result.Value.FirstName);
        Assert.Equal("Smith", result.Value.LastName);
    }

    [Fact]
    public async Task Read_ShouldReturnFail_WhenUserProfileDoesNotExist()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
        UserProfileService service = new(_logger.Object, dbContext, _fixture.AppContextAccessor);

        // Act
        Result<UserProfile> result = await service.Read();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User profile was not found", result.Errors[0].Message);
    }
}