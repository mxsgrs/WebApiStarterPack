namespace Starter.Store.Domain.Aggregates.OrderAggregate;

public class Order : IAggregateRoot
{
    public OrderId Id { get; private set; }
    public UserId UserId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreationDate { get; private set; }
    
    public Order(OrderId id, UserId userId, decimal totalAmount)
    {
        if (id.Value == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty.");
        }

        if (userId.Value == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty.");
        }

        if (totalAmount <= 0)
        {
            throw new ArgumentException("TotalAmount must be greater than zero.");
        }

        Id = id;
        UserId = userId;
        TotalAmount = totalAmount;
        Status = OrderStatus.Pending;
        CreationDate = DateTime.Now;
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }
}
