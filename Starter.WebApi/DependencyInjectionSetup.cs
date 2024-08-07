namespace Starter.WebApi;

/// <summary>
/// Tool for registering services
/// </summary>
public static class DependencyInjectionSetup
{
    /// <summary>
    /// Register application services
    /// </summary>
    /// <param name="services">Application service collection</param>
    public static void AddStarterServices(this IServiceCollection services)
    {
        services.AddScoped<IAppContextAccessor, AppContextAccessor>();
        services.AddScoped<IUserCredentialsService, UserCredentialsService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
    }
}
