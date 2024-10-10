using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

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
    builder.Services.AddDbContext<AccountDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else if (builder.Environment.IsDevelopment())
{
    builder.AddServiceDefaults();
    builder.AddSqlServerDbContext<AccountDbContext>("accountsqldb");
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


// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddStarterServices();

builder.Services.AddEndpointsApiExplorer();

// Configure JWT authentication
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        JsonWebTokenParameters jwt = builder.Configuration
            .GetRequiredSection("JsonWebTokenParameters")
            .Get<JsonWebTokenParameters>()
                ?? throw new Exception("JWT settings are not configured");

        byte[] encodedKey = Encoding.ASCII.GetBytes(jwt.Key);
        SymmetricSecurityKey symmetricSecurityKey = new(encodedKey);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = symmetricSecurityKey
        };
    });

// Read version number from application settings
string version = builder.Configuration.GetValue<string>("Version")
    ?? throw new Exception("Version number is missing");

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(version, new OpenApiInfo
    {
        Version = version,
        Title = "Starter.Account.WebApi",
        Description = "Get your starter web API."
    });
});

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger(options =>
{
    options.RouteTemplate = "api/account/swagger/{documentname}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/api/account/swagger/{version}/swagger.json", version);
    options.RoutePrefix = "api/account/swagger";
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
