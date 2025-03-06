using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models.Order;

namespace IntercityTaxi.Application.Service;

public class TripTypeService(
    ITripTypeRepository tripTypeRepository)
{
    private readonly ITripTypeRepository _tripTypeRepository = tripTypeRepository;

    public async Task<Result<TripType>> CreateAsync(string tripName)
    {
        var existingTripType = await _tripTypeRepository.GetByName(tripName);
        if (!existingTripType.IsSuccess)
            return Result<TripType>.Failure(existingTripType.Error);

        var resultCreateTripType = TripType.Create(tripName);
        if (!resultCreateTripType.IsSuccess)
        {
            return Result<TripType>.Failure(resultCreateTripType.Error);
        }

        var resultAddToDB = await _tripTypeRepository.Create(resultCreateTripType.Value);
        if (!resultAddToDB.IsSuccess)
        {
            return Result<TripType>.Failure(resultAddToDB.Error);
        }

        return Result<TripType>.Success(resultCreateTripType.Value);
    }
    public async Task<Result<List<TripType>>> GetAsync()
    {
        return await _tripTypeRepository.Get();
    }

    public async Task<Result<TripType>> GetAsyncByGuid(Guid tripTypeId)
    {
        return await _tripTypeRepository.GetByGuid(tripTypeId);
    }

    public async Task<Result<string>> DeleteAsync(Guid tripTypeId)
    {
        var existingTrip = await _tripTypeRepository.GetByGuid(tripTypeId);
        if (!existingTrip.IsSuccess)
            return Result<string>.Failure(existingTrip.Error);

        return await _tripTypeRepository.Delete(existingTrip.Value);
    }
}
