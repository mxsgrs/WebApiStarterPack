using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Starter.WebApi.IntegrationTests.Facts.Factories;

public class StarterWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<StarterContext>));

            string connectionString = _dbContainer.GetConnectionString();
            services.AddDbContext<StarterContext>(options => options
                .UseSqlServer(connectionString));
        });
    }

    public StarterContext MigrateDbContext()
    {
        IServiceScope scope = Services.CreateScope();
        StarterContext dbContext = scope.ServiceProvider.GetRequiredService<StarterContext>();
        dbContext.Database.Migrate();

        return dbContext;
    }

    public Task InitializeAsync()
        => _dbContainer.StartAsync();

    public new Task DisposeAsync()
        => _dbContainer.DisposeAsync().AsTask();
}

public static class Auth
{
    public static string Jwt { get; set; } = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzNTA0MGQ3My04OGI4LTQ4NGEtYTZlNy0yNWFiZTNhOWIwNTIiLCJzdWIiOiIxIiwiZW1haWwiOiJ0ZXN0dXNlckBnbWFpbC5jb20iLCJhdWQiOiJodHRwczovL3N0YXJ0ZXJ3ZWJhcGkuY29tIiwiaXNzIjoiaHR0cHM6Ly9zdGFydGVyd2ViYXBpLmNvbSIsImV4cCI6MTcyMzc1NjQ3Nn0.0JqIIBuhbtfM_7G9d78Z95E4kO2eQv11PVcknkdS2WmrSVfUrdvXWHyX_K2VBwwYkHOk0pXX9lP2HWBYDV3WHg";
}

public static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNameCaseInsensitive = true
    };
}