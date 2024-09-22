namespace Starter.WebApi;

public static class DependencyInjectionSetup
{
    public static void AddStarterServices(this IServiceCollection services)
    {
        services.AddScoped<IAppContextAccessor, AppContextAccessor>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();
    }
}
