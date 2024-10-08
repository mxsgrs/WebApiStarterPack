namespace Starter.Account.WebApi.Controllers;

public class UserController(IMapper mapper, IUserRepository userRepository) 
    : AccountControllerBase(mapper)
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet]
    public async Task<IActionResult> ReadUser()
    {
        Result<User> result = await _userRepository.Read();

        return CorrespondingStatus<User, UserDto>(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        Result<User> result = await _userRepository.CreateOrUpdate(user);

        return CorrespondingStatus<User, UserDto>(result);
    }
}
