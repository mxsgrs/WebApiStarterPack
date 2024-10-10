using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Starter.Store.WebApi.Utilities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add specific configuration file for the current build configuration
string configurationName = Assembly.GetExecutingAssembly()
    .GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration
        ?? throw new Exception("Can not read configuration name");

builder.Configuration.AddJsonFile($"appsettings.{configurationName}.json");

builder.AddServiceDefaults();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
