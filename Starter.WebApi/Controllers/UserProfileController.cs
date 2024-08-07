namespace Starter.WebApi.Controllers;

/// <summary>
/// Handle the application users
/// </summary>
/// <param name="userProfileService">User profile CRUD operations</param>
/// <param name="mapper">AutoMapper service</param>
public class UserProfileController(IUserProfileService userProfileService, IMapper mapper) : StarterControllerBase(mapper)
{
    private readonly IUserProfileService _userProfileService = userProfileService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Create or update user information
    /// </summary>
    /// <param name="userProfileDto">User profile information</param>
    /// <returns>User new information</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate(UserProfileDto userProfileDto)
    {
        UserProfile userProfile = _mapper.Map<UserProfile>(userProfileDto);

        Result<UserProfile> result = await _userProfileService.CreateOrUpdate(userProfile);

        return CorrespondingStatus<UserProfile, UserProfileDto>(result);
    }

    /// <summary>
    /// Read user information
    /// </summary>
    /// <returns>User information</returns>
    [HttpGet]
    public async Task<IActionResult> Read()
    {
        Result<UserProfile> result = await _userProfileService.Read();

        return CorrespondingStatus<UserProfile, UserProfileDto>(result);
    }
}
