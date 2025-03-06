namespace IntercityTaxi.Domain.Models.Order;

public class OrderStatus
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private OrderStatus() { } // Для EF Core

    public OrderStatus(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
