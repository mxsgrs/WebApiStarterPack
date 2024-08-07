namespace Starter.WebApi.Services;

/// <summary>
/// User credentials CRUD operations
/// </summary>
/// <param name="logger"></param>
/// <param name="dbContext"></param>
public class UserCredentialsService(ILogger<UserCredentialsService> logger, StarterContext dbContext,
    IAppContextAccessor appContextAccessor) : IUserCredentialsService
{
    private readonly ILogger<UserCredentialsService> _logger = logger;
    private readonly StarterContext _dbContext = dbContext;
    private readonly IAppContextAccessor _appContextAccessor = appContextAccessor;

    /// <summary>
    /// Create or update <see cref="UserCredentials"/> entity
    /// </summary>
    /// <param name="newUserCredentials">Information about user account</param>
    /// <returns>User credentials created or updated</returns>
    public async Task<Result<UserCredentials>> CreateOrUpdate(UserCredentials newUserCredentials)
    {
        long userCredentialsId = _appContextAccessor.UserClaims.UserCredentialsId;

        UserCredentials? existingUserCredentials = await _dbContext.UserCredentials
            .FirstOrDefaultAsync(credentials => credentials.Id == userCredentialsId);

        if (existingUserCredentials is null)
        {
            _logger.LogInformation("Creating user credentials {NewUserCredentials}", newUserCredentials);

            // Exclude important fields and update the rest
            _dbContext.UserCredentials.Add(newUserCredentials);
            _dbContext.SaveChanges();

            return Result.Ok(newUserCredentials);
        }
        else
        {
            _logger.LogInformation("Updating user credentials {ExistingUserCredentials}", existingUserCredentials);

            // Exclude important fields and update the rest
            existingUserCredentials.EmailAddress = newUserCredentials.EmailAddress;
            existingUserCredentials.HashedPassword = newUserCredentials.HashedPassword;
            existingUserCredentials.UserRole = newUserCredentials.UserRole;
            _dbContext.SaveChanges();

            return Result.Ok(existingUserCredentials);
        }
    }

    /// <summary>
    /// Query <see cref="UserCredentials"/> entity based on id
    /// </summary>
    /// <param name="id">User credentials identifier</param>
    /// <returns>Credentials present in the database</returns>
    public async Task<Result<UserCredentials>> Read(long id)
    {
        UserCredentials? userCredentials = await _dbContext.UserCredentials
            .FirstOrDefaultAsync(item => item.Id == id);

        if (userCredentials is null)
        {
            return Result.Fail("User credentials does not exist.");
        }

        return Result.Ok(userCredentials);
    }

    /// <summary>
    /// Query <see cref="UserCredentials"/> entity based on credentials
    /// </summary>
    /// <param name="emailAddress">User email address</param>
    /// <param name="hashedPassword">User hashed password</param>
    /// <returns>Credentials present in the database</returns>
    public async Task<Result<UserCredentials>> Read(string emailAddress, string hashedPassword)
    {
        UserCredentials? userCredentials = await _dbContext.UserCredentials
            .FirstOrDefaultAsync(item => item.EmailAddress == emailAddress
                && item.HashedPassword == hashedPassword);

        if (userCredentials is null)
        {
            return Result.Fail("User credentials does not exist.");
        }

        return Result.Ok(userCredentials);
    }
}
