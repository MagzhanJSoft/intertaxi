using IntercityTaxi.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntercityTaxi.Domain.Models.Order;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CreatedById { get; private set; }
    public User CreatedBy { get; private set; } = null!;

    public Guid FromCityId { get; private set; }
    public City FromCity { get; private set; } = null!;
    public string? FromAddress { get; private set; }

    public Guid ToCityId { get; private set; }
    public City ToCity { get; private set; } = null!;
    public string? ToAddress { get; private set; }

    public DateTime Date { get; private set; }
    public Guid TripTypeId { get; private set; }
    public TripType TripType { get; private set; } = null!;
    public decimal Price { get; private set; }
    public string? Comment { get; private set; }
    public Guid CreatedByRoleId { get; private set; }
    public UserRole CreatedByRole { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActivated { get; private set; } = true;
    public bool IsDeleted { get; private set; }
    protected Order() { }

    private Order(User createdBy, City fromCity, string? fromAddress,
                 City toCity, string? toAddress, DateTime date, TripType tripType,
                 decimal price, string? comment)
    {
        Id = Guid.NewGuid();
        CreatedById = createdBy.Id;
        CreatedBy = createdBy;
        FromCityId = fromCity.Id;
        FromCity = fromCity;
        ToCityId = toCity.Id;
        ToCity = toCity;
        FromAddress = fromAddress;
        ToAddress = toAddress;
        Date = date;
        Price = price;
        Comment = comment;
        CreatedByRoleId = createdBy.RoleId;
        CreatedByRole = createdBy.Role;
        CreatedAt = DateTime.UtcNow;
        ExpirationDate = CreatedAt.AddHours(24);
        TripTypeId = tripType.Id;
        TripType = tripType;
    }

    private static Result<string> CheckParameters(City fromCity, City toCity, TripType tripType, decimal price, DateTime date)
    {
        if (fromCity == null)
        {
            return Result<string>.Failure("From city is required.");
        }

        if (toCity == null)
        {
            return Result<string>.Failure("To city is required.");
        }

        if (price <= 0)
        {
            return Result<string>.Failure("Price must be greater than 0.");
        }

        if (tripType == null)
        {
            return Result<string>.Failure("Type of trip is required.");
        }

        if (date <= DateTime.Now)
        {
            return Result<string>.Failure("Date is invalid.");
        }

        return Result<string>.Success("Date is invalid.");
    }

    public void Cancel()
    {
        IsActivated = false;
    }
    public void DeletionMark(bool mark)
    {
        IsDeleted = mark;
    }
    public bool CheckUserId(Guid userId)
    {
        return CreatedById == userId;
    }
    public static Result<Order> Create(User createdBy, City fromCity, City toCity, DateTime date, TripType tripType, decimal price,
                                               string? fromAddress = null, string? toAddress = null, string? comment = null)
    {
        //var checkResult = CheckParameters(fromCity, toCity, tripType, price, date);
        //if (!checkResult.IsSuccess)
        //{
        //    return Result<Order>.Failure(checkResult.Error);
        //}

        return Result<Order>.Success(
            new Order(
                createdBy,
                fromCity,
                fromAddress, 
                toCity,
                toAddress,
                date,
                tripType,
                price,
                comment)
            );
    }    
}
