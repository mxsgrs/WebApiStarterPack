using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Starter.Store.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        // Register database context
        string connectionString = builder.Configuration.GetConnectionString("SqlServer")
            ?? throw new Exception("Connection string is missing");

        builder.Services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register repositories
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    }
}
