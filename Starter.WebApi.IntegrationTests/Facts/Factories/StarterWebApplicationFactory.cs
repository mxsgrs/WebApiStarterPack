using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Starter.WebApi.IntegrationTests.Facts.Factories;

internal class StarterWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbInitialization? _dbInitialization;

    public delegate void DbInitialization(StarterContext dbContext);

    internal StarterWebApplicationFactory() { }

    internal StarterWebApplicationFactory(DbInitialization dbInitialization) 
    {
        _dbInitialization = dbInitialization;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<StarterContext>));

            services.AddDbContext<StarterContext>(options => options
                .ConfigureWarnings(warning => warning.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInMemoryDatabase("InMemoryDbForTesting"));

            ServiceProvider? provider = services.BuildServiceProvider();
            StarterContext dbContext = provider.GetRequiredService<StarterContext>();

            dbContext.Database.EnsureCreated();
            _dbInitialization?.Invoke(dbContext);
        });
    }
}