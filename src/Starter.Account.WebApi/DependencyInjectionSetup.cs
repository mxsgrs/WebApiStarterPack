namespace Starter.Account.WebApi;

public static class DependencyInjectionSetup
{
    public static void AddStarterServices(this IServiceCollection services)
    {
        services.AddScoped<IAppContextAccessor, AppContextAccessor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();
    }
}
