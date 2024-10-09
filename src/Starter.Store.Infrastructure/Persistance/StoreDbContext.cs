﻿using System.Reflection;

namespace Starter.Store.Infrastructure.Persistance;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
        string? aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (aspNetCoreEnvironment == "Development" || aspNetCoreEnvironment == "Integration")
        {
            Database.Migrate();
        }
    }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly? infrastructureAssembly = typeof(IAssemblyMarker).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(infrastructureAssembly);
    }
}
