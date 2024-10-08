namespace Starter.Account.WebApi.Controllers;

public class AuthenticationController(IMapper mapper, IJsonWebTokenService jsonWebTokenService) 
    : AccountControllerBase(mapper)
{
    private readonly IJsonWebTokenService _jsonWebTokenService = jsonWebTokenService;

    [AllowAnonymous]
    [HttpPost("token")]
    public async Task<IActionResult> Token(HashedLoginRequest hashedLoginRequest)
    {
        Result<LoginResponse> result = await _jsonWebTokenService.CreateToken(hashedLoginRequest);

        return CorrespondingStatus(result);
    }
}
