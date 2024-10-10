namespace Starter.Account.WebApi.Controllers;

public class UserController(IMapper mapper, IUserRepository userRepository) 
    : AccountControllerBase(mapper)
{
    private readonly IUserRepository _userRepository = userRepository;

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        Result<User> result = await _userRepository.CreateUser(user);

        return CorrespondingStatus<User, UserDto>(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadUser(Guid id)
    {
        Result<User> result = await _userRepository.GetUser(id);

        return CorrespondingStatus<User, UserDto>(result);
    }
}
