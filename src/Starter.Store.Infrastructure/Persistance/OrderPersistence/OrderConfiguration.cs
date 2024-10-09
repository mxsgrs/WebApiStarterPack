using Starter.Store.Domain.Aggregates.OrderAggregate;

namespace Starter.Store.Infrastructure.Persistance.OrderPersistence;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Status).HasConversion<string>();
    }
}
