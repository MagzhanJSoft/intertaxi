using IntercityTaxi.Domain.Interfaces;

namespace IntercityTaxi.Application.DTOs.Order;

public class RequestOrderValidator
{
    public Result<bool> Validate(RequestOrder order)
    {
        if (order == null)
            return Result<bool>.Failure("Order is invalid.");

        if (order.FromCityId == Guid.Empty)
            return Result<bool>.Failure("FromCityId is empty.");

        if (order.ToCityId == Guid.Empty)
            return Result<bool>.Failure("ToCityId is empty.");

        if (order.FromCityId == order.ToCityId)
            return Result<bool>.Failure("Ids should not be the same.");

        if (order.Date < DateTime.Now)
            return Result<bool>.Failure("Date cannot be in the past.");

        if (order.TripTypeId == Guid.Empty)
            return Result<bool>.Failure("TripTypeId is empty.");

        if (order.Price <= 0)
            return Result<bool>.Failure("Price must be greater than zero.");

        return Result<bool>.Success(true); // Все проверки пройдены
    }
}

public record RequestOrder(
    Guid FromCityId,
    string? FromAddress,
    Guid ToCityId,
    string? ToAddress,
    DateTime Date,
    Guid TripTypeId,
    decimal Price,
    string Comment
);
