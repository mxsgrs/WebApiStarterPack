# .NET 8 web API starter pack

## Introduction

New to .NET 8? Or maybe new to .NET at all? This project implements a web API with ASP.NET Core 8 with the most common features.
It is paired with a SQL Server database and a Redis cache.

## Features

### Services and dependency injection pattern

If you inspect old projects, you might find **most of the code inside controllers**. After all this it what **MVC** means. As practices have evolved,
it is now recommended to implement business logic in what we call **services**. 

Once this is done, services can be injected in controllers but also in other services. Where previously the business logic contained in a
controller was not accessible from another controller, we can now **share it everywhere** with a service.

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
        ?? throw new Exception("Configuration settings are missing");

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

## Opening