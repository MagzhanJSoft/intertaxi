namespace IntercityTaxi.Domain.Models.Order;

public class OrderHistory
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public OrderStatus PreviousStatus { get; private set; }
    public OrderStatus NewStatus { get; private set; }
    public DateTime ChangedAt { get; private set; }
    public UserRole ChangedBy { get; private set; } // "Client", "Driver", "System"

    private OrderHistory(Guid orderId, OrderStatus previousStatus, OrderStatus newStatus, UserRole changedBy)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        PreviousStatus = previousStatus;
        NewStatus = newStatus;
        ChangedAt = DateTime.UtcNow;
        ChangedBy = changedBy;
    }

    public static OrderHistory Create(Guid orderId, OrderStatus previousStatus, OrderStatus newStatus, UserRole changedBy)
    {
        return new OrderHistory(orderId, previousStatus, newStatus, changedBy);
    }
}
