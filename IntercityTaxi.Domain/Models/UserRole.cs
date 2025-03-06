namespace IntercityTaxi.Domain.Models;

public class UserRole
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public UserRole(string name = "Client")
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
