using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Starter.Account.WebApi.IntegrationTests.Facts.Factories;

public class StarterWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Integration");

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AccountDbContext>));

            string connectionString = _dbContainer.GetConnectionString();
            services.AddDbContext<AccountDbContext>(options => options
                .UseSqlServer(connectionString));
        });
    }

    public HttpClient CreateAuthorizedClient()
    {
        HttpClient httpClient = CreateClient();

        // Expiration date is around 2034
        string jwt = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJiM2UzZjAyYS0xNzU2LTQ1NzUtODNjZS04MGI2NjE2M2QzYmUiLCJzdWIiOiIxIiwiZW1haWwiOiJqb2huLmRvZUBnbWFpbC5jb20iLCJhdWQiOiJodHRwczovL3N0YXJ0ZXJ3ZWJhcGkuY29tIiwiaXNzIjoiaHR0cHM6Ly9zdGFydGVyd2ViYXBpLmNvbSIsImV4cCI6MjAzOTI3MDA1Mn0.aeUd-y_mUKKEXSLh4JQrXV7fRw2oqcAPmcrjnXfYxpeV1f6afMCSCPrIlUJ-v8fJg4TX-r8zBQK9yyyIFTo4BA";
        httpClient.DefaultRequestHeaders.Authorization = new("Bearer", jwt);

        return httpClient;
    }

    public AccountDbContext AccessDbContext()
    {
        IServiceScope scope = Services.CreateScope();
        AccountDbContext dbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
        return dbContext;
    }

    public async Task InitializeAsync()
        => await _dbContainer.StartAsync();

    public new async Task DisposeAsync()
        => await _dbContainer.DisposeAsync().AsTask();
}

public static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNameCaseInsensitive = true
    };
}