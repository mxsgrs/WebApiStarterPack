namespace Starter.Store.Domain.Aggregates.OrderAggregate;

public class Order : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreationDate { get; private set; }

    public Order(Guid userId, decimal totalAmount)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty.");
        }

        if (totalAmount <= 0)
        {
            throw new ArgumentException("TotalAmount must be greater than zero.");
        }

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
