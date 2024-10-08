using System.Text.Json.Serialization;

namespace Starter.Store.Domain.OrderAggregate;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
}
