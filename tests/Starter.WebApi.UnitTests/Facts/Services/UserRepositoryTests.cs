namespace Starter.WebApi.UnitTests.Facts.Services;

public class UserRepositoryTests(SharedFixture sharedFixture) : IClassFixture<SharedFixture>
{
    private readonly Mock<ILogger<UserRepository>> _loggerMock = new();
    private readonly SharedFixture _sharedFixture = sharedFixture;

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenUserIsCreated()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
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
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        UserRepository userRepository = new(_loggerMock.Object, dbContext, 
            _sharedFixture.AppContextAccessor);

        // Act
        Result<User> result = await userRepository.CreateOrUpdate(user);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenUserIsUpdated()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
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
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        UserRepository userRepository = new(_loggerMock.Object, dbContext,
            _sharedFixture.AppContextAccessor);

        User newUser = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "hashedpassword123",
            FirstName = "Bob",
            LastName = "Doe",
            Birthday = new DateOnly(1990, 5, 15),
            Gender = Gender.Male,
            Role = Role.Admin,
            Phone = "+1234567890",
            UserAddress = new UserAddress
            {
                AddressLine = "123 Main St",
                AddressSupplement = "Apt 4B",
                City = "New York",
                ZipCode = "12345",
                StateProvince = "State",
                Country = "Country"
            }
        };

        // Act
        Result<User> result = await userRepository.CreateOrUpdate(newUser);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
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
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        UserRepository userRepository = new(_loggerMock.Object, dbContext,
            _sharedFixture.AppContextAccessor);

        // Act
        Result<User> result = await userRepository.Read();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task Read_ShouldFail_WhenUserDoesNotExist()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();

        UserRepository userRepository = new(_loggerMock.Object, dbContext,
            _sharedFixture.AppContextAccessor);

        // Act
        var result = await userRepository.Read();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User does not exist.", result.Errors.First().Message);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenCredentialsAreCorrect()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
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
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        UserRepository userRepository = new(_loggerMock.Object, dbContext,
            _sharedFixture.AppContextAccessor);

        // Act
        var result = await userRepository.Read("john.doe@example.com", "hashedpassword123");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task Read_ShouldFail_WhenCredentialsAreIncorrect()
    {
        // Arrange
        StarterContext dbContext = SharedFixture.CreateDatabaseContext();
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
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        UserRepository userRepository = new(_loggerMock.Object, dbContext,
            _sharedFixture.AppContextAccessor);

        // Act
        var result = await userRepository.Read("john.doe@example.com", "wrongPassword");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User does not exist.", result.Errors.First().Message);
    }
}

