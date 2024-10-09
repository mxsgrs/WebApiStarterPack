using System.Text.Json.Serialization;

namespace Starter.Store.Domain.Aggregates.OrderAggregate;

public class Order : IAggregateRoot
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
}
