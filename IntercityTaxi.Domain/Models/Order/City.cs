using IntercityTaxi.Domain.Interfaces;
using System;

namespace IntercityTaxi.Domain.Models.Order;

public class City
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private City() { }
    private City(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Result<City> Create(string name)
    {
        return Result<City>.Success(new City(Guid.NewGuid(), name));
    }


}
