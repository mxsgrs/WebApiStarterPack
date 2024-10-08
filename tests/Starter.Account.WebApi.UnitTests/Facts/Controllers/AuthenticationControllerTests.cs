namespace Starter.Account.WebApi.UnitTests.Facts.Controllers;

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
        _controller = new AuthenticationController(_mapperMock.Object, _jsonWebTokenServiceMock.Object);
    }

    [Fact]
    public async Task Token_ShouldReturnBadRequest_WhenTokenCreationFails()
    {
        // Arrange
        HashedLoginRequest request = new()
        {
            EmailAddress = "john.doe@example.com",
            HashedPassword = "TWF0cml4UmVsb2FkZWQh"
        };
        Result<LoginResponse> result = Result.Fail("Error");

        _jsonWebTokenServiceMock.Setup(s => s.CreateToken(request)).ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.Token(request);

        // Assert
        BadRequestObjectResult? badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
        Assert.Equal(result.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task Token_ShouldReturnOk_WhenTokenCreated()
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

        _jsonWebTokenServiceMock.Setup(s => s.CreateToken(request)).ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.Token(request);

        // Assert
        OkObjectResult? okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(loginResponse, okResult.Value);
    }
}

