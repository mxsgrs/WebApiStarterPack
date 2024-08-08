# .NET 8 web API starter pack

## Introduction

This project implements a web API with ASP.NET Core 8 with the most common features. It is paired with a SQL Server database using a code first approach.

## Features

### Services and dependency injection pattern

If you inspect old projects, you might find **most of the code inside controllers**. After all this it what **MVC** means. As practices have evolved,
it is now recommended to implement business logic in what we call **services**. 

Once this is done, services can be injected in controllers but also in other services. Where previously the business logic contained in a
controller was not accessible from another controller, we can now **share it everywhere** with a service.

In this project services are declared in **DependencyInjectionSetup.cs**

```csharp
public static class DependencyInjectionSetup
{
    public static void AddStarterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserCredentialsService, UserCredentialsService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
    }
}
```

This method is called in **Program.cs** like this `builder.Services.AddStarterServices();`

### JWT authentication

As JWT authentication is the standard **approach** to secure a web API, it's quite a good place to start. This project implements the whole process.
- **Send your credentials with a post requests** to an anonymous endpoint and get your token in return. 
- Now you can **access others endpoints** by adding this token to the authorization header of your HTTP requests.

JWT authentication is declared in Program.cs as following.

```csharp
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        Jwt jwt = builder.Configuration.GetRequiredSection("Jwt").Get<Jwt>()
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
```

### Mulitple configurations

Maybe you need an application that has more than the usual Debug and Release configuration. Let's say you have a **custom configuration**
for one of your clients. We will call this configuration Custom. That being said you might have different settings for this configuration and
create an **appsettings.Custom.json file**. Now you expect the application to use these settings when you **publish** it with the Custom configuration.

But this is not how .NET works. No matter what configuration you select during publishing, the application will look for **appsettings.environmentName.json**
during its execution. For example appsettings.Development.json or appsettings.Production.json for respectively Debug and Release configuration.

This project has code in Program.cs to **handle this situation**. Hence if you publish your application with Custom configuration, it will look for
appsettings.Custom.json and so on.

```csharp
string configurationName = Assembly.GetExecutingAssembly()
    .GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration
        ?? throw new Exception("Can not read configuration name");

builder.Configuration.AddJsonFile($"appsettings.{configurationName}.json");
```

### Result pattern

As **throwing exception is not right way** to handle unexpected behaviour, we prefer to return an error instead. This can be done using what
is called the **result pattern**. In this project I am using the **FluentResults** nuget package to implement this pattern.

```csharp
public async Task<Result<UserCredentials>> Read(long id)
{
    UserCredentials? userCredentials = await _dbContext.UserCredentials
        .FirstOrDefaultAsync(item => item.Id == id);

    if (userCredentials is null)
    {
        return Result.Fail("User credentials does not exist.");
    }

    return Result.Ok(userCredentials);
}
```

Another positive aspect is to **avoid returning null objects** which make the application prone to **null exceptions**.
Now that service is returning an encapsulated result and controller must return a response. It is a good thing to
create a method in an **abstract controller**, so it can be reused in every controller, that handle this. 
This way the **HTTP response status** always match the **service result**.

```csharp
[NonAction]
[ApiExplorerSettings(IgnoreApi = true)]
public IActionResult CorrespondingStatus<T>(Result<T> result)
{
    if (result.IsFailed)
    {
        return BadRequest(result.Errors);
    }

    return Ok(result.Value);
}
```

### HTTP files

While Postman is really great, having a HTTP client with all your predefined requests **inside your project** is such a handy tool. It allows to 
bind your code to those requests **in the version control**. Hence when members of the team pull your code, they instantly have the possibility 
to test it with your HTTP requests, saving time and making collaboration easier.

HTTP requests are defined in .http files. Examples for this project can be found in the **Https** folder. Each file corresponds to a controller. There 
a still some limitations, it is not possible to add pre-request or post-response scripts like in Postman, but again this feature is young and will grow.

```http
POST {{HostAddress}}/Authentication/CreateJwtBearer
Content-Type: application/json

{
  "emailAddress": "robert.durand@gmail.com",
  "hashedPassword": "369b62d459de8a74683f87c276ff8a264d6b247add4beaa02a1c7f9f3134f495"
}
```

Variables, which are between double curly braces, can be defined in the **http-client.env.json file**. Multiple
environments can be configured, making possible to attribute a different value to a variable for each environment. Then it is easy to switch between environment
with the same request, making the workflow even faster. Note that everytime this file is modified, **closing and reopening** Visual Studio is needed so changes 
are taken into account. I hope Microsoft will fix this in the future.

```json
{
  "dev": {
    "HostAddress": "https://localhost:7137",
    "Jwt": "xxx.yyy.zzz"
  },
  "prod": {
    "HostAddress": "https://starterwebapi.com",
    "Jwt": "xxx.yyy.zzz"
  }
}
```

See official Microsoft documentation for more information [here](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-8.0).

### Code first approach

In this example we are using a SQL Server 2022 database with a code first approach.

In the first place, **connection string** of an existing database is added in **appsettings.json** as following.

```json
"ConnectionStrings": {
    "SqlServer": "Data Source=localhost,1433;Initial Catalog=Starter;User=sa;Password=MatrixReloaded!;TrustServerCertificate=yes"
}
```

Then **database context and model classes are created** inside Models folder. This context is **registered as a service** 
inside Program.cs like this.

```csharp
string connectionString = builder.Configuration.GetConnectionString("SqlServer")
    ?? throw new Exception("Connection string is missing");

builder.Services.AddDbContext<StarterContext>(options =>
        options.UseSqlServer(connectionString));
```

Once all above is done, it is possible apply this structure to the running database. First step consist to create a new
migration with this PowerShell command. New .cs files describing every table will be generated in Migrations folder.

```bash
dotnet ef migrations add InitialCreate
```

When it's done, migration can be applied to the database with this command. EntityFramework will use the connection string
in order to connect to the running database and apply changes contained in the previously generated migration files.

```bash
dotnet ef database update
```

### Database Docker image

Building a Docker image of the database, all migration scripts included, is a good choice. By leveraging the benefits of container, database 
can be deployed in an instant in any environment. Anyone who get this project needs only to press run to get a web API with its fully configured
database running in containers. Here is the following database Dockerfile allowing to build this image.

```dockerfile
FROM mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04

# Set environment variables
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=B1q22MPXUgosXiqZ
ENV MSSQL_PID=Express

# Create a config directory
USER root
RUN mkdir -p /usr/src/app
RUN chown mssql /usr/src/app

# Copy the initialization script to the container
COPY *.sql /usr/src/app/
COPY *.sh /usr/src/app/
RUN chmod +x /usr/src/app/DbBuilder.sh
USER mssql

# Expose the SQL Server port
EXPOSE 1433

# Run the SQL Server process and the initialization script
CMD /bin/bash /usr/src/app/DbBuilder.sh & /opt/mssql/bin/sqlservr
```

A shell script is used to run SQL scripts in order.

```shell
#!/bin/bash
# Parameters
SQLCMD_PATH="/opt/mssql-tools18/bin/sqlcmd"
SERVER="localhost"
USERNAME="sa"
PASSWORD="B1q22MPXUgosXiqZ"
DATABASE="Starter"
# Execution
echo "Waiting for SQL Server to start"
sleep 30s
echo "Running DbCreation.sql"
$SQLCMD_PATH -C -S $SERVER -U $USERNAME -P $PASSWORD -d master -i /usr/src/app/DbCreation.sql
echo "Running InitialCreate.sql"
$SQLCMD_PATH -C -S $SERVER -U $USERNAME -P $PASSWORD -d $DATABASE -i /usr/src/app/InitialCreate.sql
```

A local instance of the database can be run with LocalDatabase.bat which its content is this.

```bat
@echo off

:: Pull SQL Server image
docker pull mxsgrs/startedb:v1.0.0

:: Run a SQL Server container
docker run -p 1433:1433 --name starterdb -d mxsgrs/starterdb:v1.0.0
```

When creating a new migration, corresponding SQL script needs to be generated so when a new Docker image of the database
is built, the latest migration is included. Use this command for generating a script based on migrations.

```bash
dotnet ef migrations script --output ./Migrations/Script.sql
```

### Logging

The most common error when developing a web API is to post an **invalid object** and get a **bad request** response in return. When this happens the developer 
needs to investigate the **ModelState**, but it can be a long and painful process. Fortunately, it is now possible to automatically log ModelState errors and
see the **relevant details**, particularly which **object property** is causing the invalid state.

```csharp
builder.Services.AddControllers()
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
                logger.LogError("{ErrorMessage}", error.ErrorMessage);
            }

            return builtInFactory(context);
        };
    });
```

As this is logging, it will appear in every configured sink. One of them being the console which is always open, it allows developers to see this 
type of content immediately. Here is an example.

```
2024-08-08 18:10:01 fail: Program[0]
2024-08-08 18:10:01       The EmailAddress field is required.
```

### Global usings

With the new feature `global using`, namespaces can be included for the whole project instead having to specify it in every file. This feature improve 
maintainability and save time on repetitive tasks. Implementation can be found in **GlobalUsing.cs** file, inside each project root folder.

```csharp
global using Starter.WebApi;
global using Starter.WebApi.Controllers.Abstracts;
global using Starter.WebApi.Models.Authentication;
global using Starter.WebApi.Models.Database;
global using Starter.WebApi.Models.DataTransferObjects;
global using Starter.WebApi.Services;
global using Starter.WebApi.Services.Interfaces;
```

## Opening

This project does not cover everything of course. It aims to provide the basics and get you going quickly, so you can dive into more complex structure 
faster. Evolving to a Domain Driven Design is possibility or using it in a microservice environment is another one.
