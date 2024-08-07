namespace Starter.WebApi.Models.Database;

public partial class StarterContext : DbContext
{
    public StarterContext(DbContextOptions<StarterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserCredentials> UserCredentials { get; set; }

    public virtual DbSet<UserProfile> UserProfile { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCredentials>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserCredentials_pkey");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserProfile_pkey");

            entity.HasOne(d => d.UserCredentials).WithMany(p => p.UserProfile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserProfile_UserCredentialsId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
