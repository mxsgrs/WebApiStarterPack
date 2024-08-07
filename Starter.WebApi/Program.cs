using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add specific configuration file for the current build configuration
string configurationName = Assembly.GetExecutingAssembly()
    .GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration
        ?? throw new Exception("Configuration settings are missing");

builder.Configuration.AddJsonFile($"appsettings.{configurationName}.json");

// Add services to the container
builder.Services.AddHttpContextAccessor();

// Get database connection string
string connectionString = builder.Configuration.GetConnectionString("AdmxAccount")
    ?? throw new Exception("Connection string is missing");

// Add controllers and serialization
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStarterServices();

string version = builder.Configuration.GetValue<string>("Version")
    ?? throw new Exception("Version number is missing");

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(version, new OpenApiInfo
    {
        Version = version,
        Title = "Admx.Account.WebApi",
        Description = "Access to Admx services."
    });
});

WebApplication app = builder.Build();

app.UseSwagger(options =>
{
    options.RouteTemplate = "account/swagger/{documentname}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/account/swagger/{version}/swagger.json", version);
    options.RoutePrefix = "account/swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
