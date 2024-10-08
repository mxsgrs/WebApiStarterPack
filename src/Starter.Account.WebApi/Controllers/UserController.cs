﻿namespace Starter.Account.WebApi.Controllers;

public class UserController(IMapper mapper, IAppContextAccessor appContextAccessor, IUserRepository userRepository) 
    : AccountControllerBase(mapper)
{
    private readonly IMapper _mapper = mapper;
    private readonly IAppContextAccessor _appContextAccessor = appContextAccessor;
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet]
    public async Task<IActionResult> ReadUser()
    {
        Guid id = _appContextAccessor.UserClaims.Id;

        Result<User> result = await _userRepository.GetUser(id);

        return CorrespondingStatus<User, UserDto>(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        Result<User> result = await _userRepository.CreateUser(user);

        return CorrespondingStatus<User, UserDto>(result);
    }
}
