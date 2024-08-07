namespace Starter.WebApi.Services;

/// <summary>
/// User profile CRUD operations
/// </summary>
/// <param name="logger">Logging interface</param>
/// <param name="dbContext">Database context</param>
/// <param name="appContextAccessor">Application context</param>
public class UserProfileService(ILogger<UserProfileService> logger, AdmxAccountContext dbContext,
    IAppContextAccessor appContextAccessor) : IUserProfileService
{
    private readonly ILogger<UserProfileService> _logger = logger;
    private readonly AdmxAccountContext _dbContext = dbContext;
    private readonly IAppContextAccessor _appContextAccessor = appContextAccessor;

    /// <summary>
    /// Update user informations
    /// </summary>
    /// <param name="userProfile"></param>
    /// <returns>Updated user profile</returns>
    public Task<UserProfile?> CreateOrUpdate(UserProfile userProfile)
    {
        long userCredentialsId = _appContextAccessor.UserClaims.UserCredentialsId;

        UserProfile? oldUserProfile = _dbContext.UserProfile?
            .FirstOrDefault(profile => profile.UserCredentialsId == userCredentialsId);

        if (oldUserProfile is null)
        {
            _logger.LogInformation("Creating user {UserProfile}", userProfile);

            // Exclude important fields and update the rest
            _dbContext.UserProfile?.Add(userProfile);
            _dbContext.SaveChanges();

            return Task.FromResult<UserProfile?>(userProfile);
        }
        else
        {
            _logger.LogInformation("Updating user {UserProfile}", userProfile);

            // Exclude important fields and update the rest
            oldUserProfile.AddressSupplement = userProfile.AddressSupplement;
            oldUserProfile.Birthday = userProfile.Birthday;
            oldUserProfile.City = userProfile.City;
            oldUserProfile.Country = userProfile.Country;
            oldUserProfile.EmailAddress = userProfile.EmailAddress;
            oldUserProfile.FirstName = userProfile.FirstName;
            oldUserProfile.Gender = userProfile.Gender;
            oldUserProfile.LastName = userProfile.LastName;
            oldUserProfile.PersonalPhone = userProfile.PersonalPhone;
            oldUserProfile.Position = userProfile.Position;
            oldUserProfile.PostalAddress = userProfile.PostalAddress;
            oldUserProfile.ProfessionalPhone = userProfile.ProfessionalPhone;
            oldUserProfile.StateProvince = userProfile.StateProvince;
            oldUserProfile.ZipCode = userProfile.ZipCode;
            _dbContext.SaveChanges();

            return Task.FromResult<UserProfile?>(oldUserProfile);
        }
    }

    /// <summary>
    /// Read a user informations
    /// </summary>
    /// <returns>Existing user profile</returns>
    public Task<UserProfile?> Read()
    {
        long userCredentialsId = _appContextAccessor.UserClaims.UserCredentialsId;

        UserProfile? userProfile = _dbContext.UserProfile
            .FirstOrDefault(item => item.UserCredentialsId == userCredentialsId);

        _logger.LogInformation("Reading user profile {UserProfile}", userProfile);

        return Task.FromResult(userProfile);
    }
}
