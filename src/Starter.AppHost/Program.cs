var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Starter_Account_WebApi>("starter-account-webapi");

builder.AddProject<Projects.Starter_Store_WebApi>("starter-store-webapi");

builder.Build().Run();
