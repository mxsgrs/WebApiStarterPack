namespace Starter.Store.WebApi.Models.DataTransferObjects;

public record OrderDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public decimal TotalAmount { get; init; }
    public OrderStatus Status { get; init; }
    public DateTime CreationDate { get; init; }
}
