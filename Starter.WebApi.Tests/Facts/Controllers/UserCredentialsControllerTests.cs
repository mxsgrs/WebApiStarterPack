namespace Starter.WebApi.Tests.Facts.Controllers;

public class UserCredentialsControllerTests
{
    private readonly Mock<IUserCredentialsService> _mockUserCredentialsService;
    private readonly IMapper _mapper;
    private readonly UserCredentialsController _controller;

    public UserCredentialsControllerTests()
    {
        MapperConfiguration config = new(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();
        _mockUserCredentialsService = new Mock<IUserCredentialsService>();
        _controller = new UserCredentialsController(_mockUserCredentialsService.Object, _mapper);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        UserCredentials userCredentials = new() 
        { 
            EmailAddress = "john.doe@gmail.com",
            HashedPassword = "TWF0cml4UmVsb2FkZWQh",
            UserRole = "admin"
        };
        UserCredentialsDto userCredentialsDto = _mapper.Map<UserCredentialsDto>(userCredentials);
        Result<UserCredentials> result = Result.Ok(userCredentials);

        _mockUserCredentialsService.Setup(s => s.CreateOrUpdate(It.IsAny<UserCredentials>()))
                                   .ReturnsAsync(result);

        // Act
        IActionResult actionResult = await _controller.CreateOrUpdate(userCredentialsDto);

        // Assert
        Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnBadRequest_WhenLoginFails()
    {
        // Arrange
        UserCredentialsDto userCredentialsDto = new()
        {
            EmailAddress = "john.doe@gmail.com",
            HashedPassword = "TWF0cml4UmVsb2FkZWQh",
            UserRole = "admin"
        };
        Result<UserCredentials> result = Result.Fail<UserCredentials>("Error");

        _mockUserCredentialsService.Setup(s => s.CreateOrUpdate(It.IsAny<UserCredentials>()))
                                   .ReturnsAsync(result);

        // Act
        IActionResult actionResult = await _controller.CreateOrUpdate(userCredentialsDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenReadIsSuccessful()
    {
        // Arrange
        UserCredentials userCredentials = new()
        {
            Id = 1,
            EmailAddress = "john.doe@gmail.com",
            HashedPassword = "TWF0cml4UmVsb2FkZWQh",
            UserRole = "admin"
        };
        Result<UserCredentials> result = Result.Ok(userCredentials);

        _mockUserCredentialsService.Setup(s => s.Read())
                                   .ReturnsAsync(result);

        // Act
        IActionResult actionResult = await _controller.Read();

        // Assert
        Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public async Task Read_ShouldReturnBadRequest_WhenReadFails()
    {
        // Arrange
        Result<UserCredentials> result = Result.Fail<UserCredentials>("Error");

        _mockUserCredentialsService.Setup(s => s.Read())
                                   .ReturnsAsync(result);

        // Act
        IActionResult actionResult = await _controller.Read();

        // Assert
        Assert.IsType<BadRequestObjectResult>(actionResult);
    }
}
