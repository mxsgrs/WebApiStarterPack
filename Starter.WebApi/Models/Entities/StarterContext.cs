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

    public virtual DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.HasIndex(e => e.EmailAddress).IsUnique();
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => new { a.AddressLine, a.City, a.ZipCode, a.Country })
                .HasName("Address_pkey");

            entity.HasMany(d => d.Users).WithOne(p => p.Address)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Address_UserId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
