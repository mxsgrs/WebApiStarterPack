namespace Starter.WebApi.Models.Entities;

public partial class StarterContext : DbContext
{
    public StarterContext(DbContextOptions<StarterContext> options)
        : base(options)
    {
        string? aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (aspNetCoreEnvironment == "Development" || aspNetCoreEnvironment == "Integration")
        {
            Database.Migrate();
        }
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.HasIndex(e => e.EmailAddress).IsUnique();

            entity.Property(e => e.Role).HasConversion<string>();

            entity.Property(e => e.Gender).HasConversion<string>();

            entity.OwnsOne(x => x.UserAddress, ua =>
            {
                ua.ToTable("UserAddresses");
            });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
