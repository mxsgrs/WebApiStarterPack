using Microsoft.AspNetCore.Mvc;

namespace Starter.WebApi.Controllers;

/// <summary>
/// Handle user credentials
/// </summary>
/// <param name="userCredentialsService">User credentials CRUD operations</param>
public class UserCredentialsController(IUserCredentialsService userCredentialsService) : StarterControllerBase
{
    private readonly IUserCredentialsService _userCredentialsService = userCredentialsService;

    /// <summary>
    /// Create or update user credentials
    /// </summary>
    /// <param name="userCredentials">User's login and password</param>
    /// <returns>User credentials information</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate(UserCredentials userCredentials)
    {
        Result<UserCredentials> result = await _userCredentialsService.CreateOrUpdate(userCredentials);

        return CorrespondingStatus(result);
    }

    /// <summary>
    /// Read user credentials
    /// </summary>
    /// <param name="id">User credentials identifier</param>
    /// <returns>User credentials information</returns>
    [HttpGet]
    public async Task<IActionResult> Read(long id)
    {
        Result<UserCredentials> result  = await _userCredentialsService.Read(id);

        return CorrespondingStatus(result);
    }
}
