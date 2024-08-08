namespace Starter.WebApi.Tests.Facts.Controllers;

public class AuthenticationControllerTests
{
    [Fact]
    public async Task CreateJwtBearer_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        HashedLoginRequest hashedLoginRequest = new()
        {
            EmailAddress = "test@example.com",
            HashedPassword = "hashedpassword"
        };

        LoginResponse loginResponse = new()
        {
            AccessToken = "token"
        };

        Result<LoginResponse> result = Result.Ok(loginResponse);

        Mock<IAuthenticationService> mockAuthService = new();
        mockAuthService
            .Setup(s => s.CreateJwtBearer(hashedLoginRequest))
            .ReturnsAsync(result);

        Mock<IMapper> mapper = new();
        AuthenticationController controller = new(mockAuthService.Object, mapper.Object);

        // Act
        IActionResult actionResult = await controller.CreateJwtBearer(hashedLoginRequest);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(result.Value, okResult.Value);
    }

    [Fact]
    public async Task CreateJwtBearer_ShouldReturnUnauthorized_WhenLoginFails()
    {
        // Arrange
        HashedLoginRequest hashedLoginRequest = new()
        {
            EmailAddress = "test@example.com",
            HashedPassword = "wrongpassword"
        };

        Result<LoginResponse> result = Result.Fail("Invalid credentials");

        Mock<IAuthenticationService> mockAuthService = new();
        mockAuthService
            .Setup(s => s.CreateJwtBearer(hashedLoginRequest))
            .ReturnsAsync(result);

        Mock<IMapper> mapper = new();
        AuthenticationController controller = new(mockAuthService.Object, mapper.Object);

        // Act
        IActionResult actionResult = await controller.CreateJwtBearer(hashedLoginRequest);

        // Assert
        BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        List<IError> errorList = Assert.IsType<List<IError>>(badRequestResult.Value);
        Assert.Equal(result.Errors[0].Message, errorList[0].Message);
    }
}
