namespace Starter.Store.Infrastructure.Persistance.OrderPersistence;
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.Id)
               .ValueGeneratedNever()
               .IsRequired()
               .HasConversion(
                    id => id.Value,
                    value => new OrderId(value));

        builder.Property(e => e.Status).HasConversion<string>();
    }
}
