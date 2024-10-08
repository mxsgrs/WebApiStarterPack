namespace Starter.Account.WebApi.UnitTests.Facts.Services;

public class UserRepositoryTests
{
    private readonly Mock<ILogger<UserRepository>> _loggerMock = new();

    [Fact]
    public async Task CreateUser_ShouldReturnOk_WhenUserIsCreated()
    {
        // Arrange
        AccountDbContext dbContext = SharedFixture.CreateDatabaseContext();
        User user = new()
        {
            Id = Guid.NewGuid(),
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

        UserRepository userRepository = new(_loggerMock.Object, dbContext);

        // Act
        Result<User> result = await userRepository.CreateUser(user);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnOk_WhenUserIsUpdated()
    {
        // Arrange
        AccountDbContext dbContext = SharedFixture.CreateDatabaseContext();
        Guid id = Guid.NewGuid();
        User user = new()
        {
            Id = id,
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

        UserRepository userRepository = new(_loggerMock.Object, dbContext);

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
        Result<User> result = await userRepository.UpdateUser(id, newUser);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
        AccountDbContext dbContext = SharedFixture.CreateDatabaseContext();
        Guid id = Guid.NewGuid();
        User user = new()
        {
            Id = id,
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

        UserRepository userRepository = new(_loggerMock.Object, dbContext);

        // Act
        Result<User> result = await userRepository.GetUser(id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task Read_ShouldFail_WhenUserDoesNotExist()
    {
        // Arrange
        AccountDbContext dbContext = SharedFixture.CreateDatabaseContext();
        UserRepository userRepository = new(_loggerMock.Object, dbContext);

        // Act
        var result = await userRepository.GetUser(Guid.NewGuid());

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User does not exist.", result.Errors.First().Message);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenCredentialsAreCorrect()
    {
        // Arrange
        AccountDbContext dbContext = SharedFixture.CreateDatabaseContext();
        User user = new()
        {
            Id = Guid.NewGuid(),
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

        UserRepository userRepository = new(_loggerMock.Object, dbContext);

        // Act
        var result = await userRepository.GetUser("john.doe@example.com", "hashedpassword123");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public async Task Read_ShouldFail_WhenCredentialsAreIncorrect()
    {
        // Arrange
        AccountDbContext dbContext = SharedFixture.CreateDatabaseContext();
        User user = new()
        {
            Id = Guid.NewGuid(),
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

        UserRepository userRepository = new(_loggerMock.Object, dbContext);

        // Act
        var result = await userRepository.GetUser("john.doe@example.com", "wrongPassword");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User does not exist.", result.Errors.First().Message);
    }
}

