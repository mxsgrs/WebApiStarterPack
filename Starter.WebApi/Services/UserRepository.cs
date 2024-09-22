﻿namespace Starter.WebApi.Services;

public class UserRepository(ILogger<UserRepository> logger, StarterContext dbContext,
    IAppContextAccessor appContextAccessor) : IUserRepository
{
    private readonly ILogger<UserRepository> _logger = logger;
    private readonly StarterContext _dbContext = dbContext;
    private readonly IAppContextAccessor _appContextAccessor = appContextAccessor;

    /// <summary>
    /// Create or update an user
    /// </summary>
    public async Task<Result<User>> CreateOrUpdate(User user)
    {
        long id = _appContextAccessor.UserClaims.Id;

        User? existing = await _dbContext.Users.FindAsync(id);

        if (existing is null)
        {
            _logger.LogInformation("Creating user credentials {User}", user);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Result.Ok(user);
        }
        else
        {
            _logger.LogInformation("Updating user credentials {Existing}", existing);

            _dbContext.Entry(existing).CurrentValues.SetValues(user);
            _dbContext.SaveChanges();

            return Result.Ok(existing);
        }
    }

    /// <summary>
    /// Read an existing user
    /// </summary>
    public async Task<Result<User>> Read()
    {
        long id = _appContextAccessor.UserClaims.Id;

        User? user = await _dbContext.Users.FindAsync(id);

        if (user is null)
        {
            return Result.Fail("User does not exist.");
        }

        return Result.Ok(user);
    }

    /// <summary>
    /// Read an existing user base on his credentials
    /// </summary>
    public async Task<Result<User>> Read(string emailAddress, string hashedPassword)
    {
        User? user = await _dbContext.Users
            .FirstOrDefaultAsync(item => item.EmailAddress == emailAddress
                && item.HashedPassword == hashedPassword);

        if (user is null)
        {
            return Result.Fail("User does not exist.");
        }

        return Result.Ok(user);
    }
}
