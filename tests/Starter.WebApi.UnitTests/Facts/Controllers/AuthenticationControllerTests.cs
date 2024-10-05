namespace Starter.WebApi.UnitTests.Facts.Controllers;

public class AuthenticationControllerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJsonWebTokenService> _jsonWebTokenServiceMock;
    private readonly AuthenticationController _controller;

    public AuthenticationControllerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _jsonWebTokenServiceMock = new Mock<IJsonWebTokenService>();
        _controller = new AuthenticationController(_mapperMock.Object, 
            _userRepositoryMock.Object, _jsonWebTokenServiceMock.Object);
    }

    [Fact]
    public async Task GenerateToken_ShouldReturnBadRequest_WhenTokenCreationFails()
    {
        // Arrange
        HashedLoginRequest request = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "TWF0cml4UmVsb2FkZWQh"
        };
        Result<LoginResponse> result = Result.Fail("Error");

        _jsonWebTokenServiceMock.Setup(s => s.Create(request)).ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.GenerateToken(request);

        // Assert
        BadRequestObjectResult? badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
        Assert.Equal(result.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task GenerateToken_ShouldReturnOk_WhenTokenCreated()
    {
        // Arrange
        HashedLoginRequest request = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "TWF0cml4UmVsb2FkZWQh"
        };
        LoginResponse loginResponse = new()
        {
            AccessToken = "xxx.yyy.zzz"
        };
        Result<LoginResponse> result = Result.Ok(loginResponse);

        _jsonWebTokenServiceMock.Setup(s => s.Create(request)).ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.GenerateToken(request);

        // Assert
        OkObjectResult? okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(loginResponse, okResult.Value);
    }

    [Fact]
    public async Task Profile_ShouldReturnBadRequest_WhenReadFails()
    {
        // Arrange
        var result = Result.Fail("Error");

        _userRepositoryMock.Setup(r => r.Read()).ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.Profile();

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
        IActionResult response = await _controller.Profile();

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
        IActionResult response = await _controller.Register(userDto);

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
        IActionResult response = await _controller.Register(userDto);

        // Assert
        OkObjectResult? okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(userDto, okResult.Value);
    }
}

