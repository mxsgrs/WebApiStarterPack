using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Starter.Store.WebApi.OrderFeature.Infrastructure;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        // Value converter for OrderId
        var orderIdConverter = new ValueConverter<OrderId, Guid>(
            v => v.Value,
            v => new OrderId(v));

        // Value converter for UserId
        var userIdConverter = new ValueConverter<UserId, Guid>(
            v => v.Value,
            v => new UserId(v));

        builder.HasKey(o => o.Id);

        builder
            .Property(o => o.Id)
            .HasConversion(orderIdConverter).ValueGeneratedNever();

        builder
            .Property(o => o.UserId)
            .HasConversion(userIdConverter);

        builder.Property(e => e.Status).HasConversion<string>();
    }
}
