namespace Starter.WebApi.Controllers;

/// <summary>
/// Handle user credentials
/// </summary>
/// <param name="userCredentialsService">User credentials CRUD operations</param>
/// <param name="mapper">AutoMapper service</param>
public class UserCredentialsController(IUserCredentialsService userCredentialsService, IMapper mapper) 
    : StarterControllerBase(mapper)
{
    private readonly IUserCredentialsService _userCredentialsService = userCredentialsService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Create or update user credentials
    /// </summary>
    /// <param name="userCredentialsDto">User's login and password</param>
    /// <returns>User credentials information</returns>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate(UserCredentialsDto userCredentialsDto)
    {
        UserCredentials userCredentials = _mapper.Map<UserCredentials>(userCredentialsDto);

        Result<UserCredentials> result = await _userCredentialsService.CreateOrUpdate(userCredentials);

        return CorrespondingStatus<UserCredentials, UserCredentialsDto>(result);
    }

    /// <summary>
    /// Read user credentials
    /// </summary>
    /// <returns>User credentials information</returns>
    [HttpGet]
    public async Task<IActionResult> Read()
    {
        Result<UserCredentials> result  = await _userCredentialsService.Read();

        return CorrespondingStatus<UserCredentials, UserCredentialsDto>(result);
    }
}
