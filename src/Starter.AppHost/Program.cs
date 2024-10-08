var builder = DistributedApplication.CreateBuilder(args);

var storesql = builder.AddSqlServer("storesql");
var storesqldb = storesql.AddDatabase("storesqldb");

var storeapi = builder.AddProject<Projects.Starter_Store_WebApi>("starter-store-webapi")
    .WithReference(storesqldb);

var accountsql = builder.AddSqlServer("accountsql");
var accountsqldb = accountsql.AddDatabase("accountsqldb");

builder.AddProject<Projects.Starter_Account_WebApi>("starter-account-webapi")
    .WithReference(accountsqldb)
    .WithExternalHttpEndpoints()
    .WithReference(storeapi);

builder.Build().Run();
