namespace Starter.Account.WebApi.Services;

public class UserRepository(ILogger<UserRepository> logger, AccountDbContext dbContext) 
    : IUserRepository
{
    private readonly ILogger<UserRepository> _logger = logger;
    private readonly AccountDbContext _dbContext = dbContext;

    public async Task<Result<User>> CreateUser(User user)
    {
        _logger.LogInformation("Creating user credentials {User}", user);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return Result.Ok(user);
    }

    public async Task<Result<User>> GetUser(Guid id)
    {
        User? user = await _dbContext.Users.FindAsync(id);

        if (user is null)
        {
            return Result.Fail("User does not exist.");
        }

        return Result.Ok(user);
    }

    public async Task<Result<User>> GetUser(string emailAddress, string hashedPassword)
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

    public async Task<Result<User>> UpdateUser(Guid id, User user)
    {
        User? existing = await _dbContext.Users.FindAsync(id) ??
            await _dbContext.Users.FirstOrDefaultAsync(item =>
                item.EmailAddress == user.EmailAddress);

        if (existing is null)
        {
            _logger.LogInformation("User with email {Email} not found", user.EmailAddress);
            return Result.Fail("User not found");
        }

        _logger.LogInformation("Updating user credentials {Existing}", existing);

        user.Id = existing.Id;
        _dbContext.Entry(existing).CurrentValues.SetValues(user);

        // Update the user address as owned types are not updated by default
        if (user.UserAddress is not null && existing.UserAddress is not null)
        {
            _dbContext.Entry(existing.UserAddress).CurrentValues.SetValues(user.UserAddress);
        }

        await _dbContext.SaveChangesAsync();

        return Result.Ok(existing);
    }
}
