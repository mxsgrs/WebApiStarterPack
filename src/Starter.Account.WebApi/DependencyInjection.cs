namespace Starter.Account.WebApi;

public static class DependencyInjection
{
    public static void AddStarterServices(this IServiceCollection services)
    {
        services.AddScoped<IAppContextAccessor, AppContextAccessor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();
    }
}
