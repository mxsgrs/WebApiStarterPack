namespace Starter.Account.WebApi.UnitTests.Facts.Controllers;

public class UserControllerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new UserController(_mapperMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task Profile_ShouldReturnBadRequest_WhenReadFails()
    {
        // Arrange
        var result = Result.Fail("Error");

        _userRepositoryMock.Setup(r => r.Read()).ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.ReadUser();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
        Assert.Equal(result.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task Profile_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
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
        Result<User> result = Result.Ok(user);

        _userRepositoryMock.Setup(r => r.Read()).ReturnsAsync(result);
        _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        // Act
        IActionResult response = await _controller.ReadUser();

        // Assert
        OkObjectResult? okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(userDto, okResult.Value);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenCreationFails()
    {
        // Arrange
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
        Result<User> result = Result.Fail("Error");

        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);
        _userRepositoryMock.Setup(r => r.CreateOrUpdate(user)).ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.CreateUser(userDto);

        // Assert
        BadRequestObjectResult? badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
        Assert.Equal(result.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenUserCreated()
    {
        // Arrange
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
        Result<User> result = Result.Ok(user);

        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);
        _userRepositoryMock.Setup(r => r.CreateOrUpdate(user)).ReturnsAsync(result);
        _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        // Act
        IActionResult response = await _controller.CreateUser(userDto);

        // Assert
        OkObjectResult? okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(userDto, okResult.Value);
    }
}
