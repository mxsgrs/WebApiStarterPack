namespace Starter.WebApi.Controllers;

/// <summary>
/// Handle authentication processes
/// </summary>
/// <param name="authenticationService">Authentication operations</param>
/// <param name="mapper">AutoMapper service</param>
public class AuthenticationController(IAuthenticationService authenticationService, IMapper mapper) 
    : StarterControllerBase(mapper)
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    /// <summary>
    /// User give is credentials and get an access in return
    /// </summary>
    /// <param name="hashedLoginRequest"></param>
    /// <returns>JSON web token</returns>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateJwtBearer(HashedLoginRequest hashedLoginRequest)
    {
        Result<LoginResponse> result = await _authenticationService.CreateJwtBearer(hashedLoginRequest);

        return CorrespondingStatus(result);
    }
}
