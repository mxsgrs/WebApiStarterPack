namespace Starter.WebApi.UnitTests.Facts.Controllers;

public class UserProfileControllerTests
{
    private readonly Mock<IUserProfileService> _userProfileServiceMock;
    private readonly IMapper _mapper;
    private readonly UserProfileController _controller;

    public UserProfileControllerTests()
    {
        MapperConfiguration config = new(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();
        _userProfileServiceMock = new Mock<IUserProfileService>();
        _controller = new UserProfileController(_userProfileServiceMock.Object, _mapper);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnBadRequest_WhenServiceFails()
    {
        // Arrange
        UserProfileDto userProfileDto = new();
        Result<UserProfile> result = Result.Fail("Error");

        _userProfileServiceMock.Setup(x => x.CreateOrUpdate(It.IsAny<UserProfile>()))
            .ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.CreateOrUpdate(userProfileDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenServiceIsSuccessful()
    {
        // Arrange
        UserProfile userProfile = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1991, 2, 18)
        };
        UserProfileDto userProfileDto = _mapper.Map<UserProfileDto>(userProfile);
        Result<UserProfile> result = Result.Ok(userProfile);

        _userProfileServiceMock.Setup(x => x.CreateOrUpdate(It.IsAny<UserProfile>()))
            .ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.CreateOrUpdate(userProfileDto);

        // Assert
        Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public async Task Read_ShouldReturnNotFound_WhenServiceFails()
    {
        // Arrange
        Result<UserProfile> result = Result.Fail("Not Found");

        _userProfileServiceMock.Setup(x => x.Read())
            .ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.Read();

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenServiceIsSuccessful()
    {
        // Arrange
        UserProfile userProfile = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1991, 2, 18)
        };
        Result<UserProfile> result = Result.Ok(userProfile);

        _userProfileServiceMock.Setup(x => x.Read())
            .ReturnsAsync(result);

        // Act
        IActionResult response = await _controller.Read();

        // Assert
        Assert.IsType<OkObjectResult>(response);
    }
}
