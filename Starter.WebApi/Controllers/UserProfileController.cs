using Microsoft.AspNetCore.Mvc;

namespace Starter.WebApi.Controllers;

/// <summary>
/// Handle the application users
/// </summary>
/// <param name="userProfileService"></param>
public class UserProfileController(IUserProfileService userProfileService) : StarterControllerBase
{
    private readonly IUserProfileService _userProfileService = userProfileService;

    /// <summary>
    /// Create or update user information
    /// </summary>
    /// <param name="userProfile"></param>
    /// <returns>User new information</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate(UserProfile userProfile)
    {
        UserProfile? newUserProfile = await _userProfileService.CreateOrUpdate(userProfile);

        return Ok(newUserProfile);
    }

    /// <summary>
    /// Read user information
    /// </summary>
    /// <returns>User information</returns>
    [HttpGet]
    public async Task<IActionResult> Read()
    {
        UserProfile? userProfile = await _userProfileService.Read();

        return Ok(userProfile);
    }
}
