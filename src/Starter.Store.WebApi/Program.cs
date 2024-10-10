using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;
using Starter.Store.WebApi.Utilities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add specific configuration file for the current build configuration
string configurationName = Assembly.GetExecutingAssembly()
    .GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration
        ?? throw new Exception("Can not read configuration name");

builder.Configuration.AddJsonFile($"appsettings.{configurationName}.json");

string? aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (builder.Environment.IsProduction() || aspNetCoreEnvironment == "Integration")
{
    // Read database connection string from application settings
    string connectionString = builder.Configuration.GetConnectionString("SqlServer")
        ?? throw new Exception("Connection string is missing");

    // Register database context as a service
    // Connect to database with connection string
    builder.Services.AddDbContext<StoreDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else if (builder.Environment.IsDevelopment())
{
    builder.AddServiceDefaults();
    builder.AddSqlServerDbContext<StoreDbContext>("storesqldb");
}

// AutoMapper for database models and DTOs mapping
Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddAutoMapper(assemblies);

// Add controllers and serialization
builder.Services.AddControllers(options =>
{
    // Use kebab case for endpoint URLs
    ToKebabParameterTransformer toKebab = new();
    options.Conventions.Add(new RouteTokenTransformerConvention(toKebab));
})
    .ConfigureApiBehaviorOptions(options =>
    {
        var builtInFactory = options.InvalidModelStateResponseFactory;
        options.InvalidModelStateResponseFactory = context =>
        {
            ILogger<Program> logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();

            IEnumerable<ModelError> errors = context.ModelState.Values
                .SelectMany(item => item.Errors);

            foreach (ModelError error in errors)
            {
                // Logging all invalid model states
                logger.LogError("{ErrorMessage}", error.ErrorMessage);
            }

            return builtInFactory(context);
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Read version number from application settings
string version = builder.Configuration.GetValue<string>("Version")
    ?? throw new Exception("Version number is missing");

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(version, new OpenApiInfo
    {
        Version = version,
        Title = "Starter.Store.WebApi",
        Description = "Get your starter web API."
    });
});

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger(options =>
{
    options.RouteTemplate = "api/store/swagger/{documentname}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/api/store/swagger/{version}/swagger.json", version);
    options.RoutePrefix = "api/store/swagger";
});

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
