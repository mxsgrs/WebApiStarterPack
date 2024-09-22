namespace Starter.WebApi.Controllers;

public class AuthenticationController(IMapper mapper, IUserRepository userRepository, 
    IJsonWebTokenService jsonWebTokenService) : StarterControllerBase(mapper)
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJsonWebTokenService _jsonWebTokenService = jsonWebTokenService;

    /// <summary>
    /// Create a new user in the database
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        Result<User> result = await _userRepository.CreateOrUpdate(user);

        return CorrespondingStatus<User, UserDto>(result);
    }

    /// <summary>
    /// Read existing user information
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        Result<User> result = await _userRepository.Read();

        return CorrespondingStatus<User, UserDto>(result);
    }

    /// <summary>
    /// Create a JWT based on a request
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> GenerateToken(HashedLoginRequest hashedLoginRequest)
    {
        Result<LoginResponse> result = await _jsonWebTokenService.Create(hashedLoginRequest);

        return CorrespondingStatus(result);
    }
}
