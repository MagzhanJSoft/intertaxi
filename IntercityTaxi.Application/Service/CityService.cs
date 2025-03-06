using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models.Order;

namespace IntercityTaxi.Application.Service;

public class CityService(
    ICityRepository cityRepository,
    ITripTypeRepository tripTypeRepository)

{
    private readonly ICityRepository _cityRepository = cityRepository;
    private readonly ITripTypeRepository _tripTypeRepository = tripTypeRepository;
    public async Task<Result<City>> CreateAsync(string cityName)
    {
        var existingCity = await _cityRepository.GetByName(cityName);
        if (!existingCity.IsSuccess)
            return Result<City>.Failure(existingCity.Error);

        var existingTripType = await _tripTypeRepository.GetByGuid(Guid.Parse("a29b51a0-65f5-438e-8376-67a54851cfab"));
        if (!existingTripType.IsSuccess)
            return Result<City>.Failure(existingTripType.Error);

        var resultCreateCity = City.Create(cityName);
        if (!resultCreateCity.IsSuccess)
        {
            return Result<City>.Failure(resultCreateCity.Error);
        }

        var resultAddToDB = await _cityRepository.Create(resultCreateCity.Value);
        if (!resultAddToDB.IsSuccess) 
        {
            return Result<City>.Failure(resultAddToDB.Error);
        }

        return Result<City>.Success(resultCreateCity.Value);
    }
    public async Task<Result<List<City>>> GetAsync()
    {
        return await _cityRepository.Get();
    }

    public async Task<Result<City>> GetAsyncByGuid(Guid cityId)
    {
        return await _cityRepository.GetByGuid(cityId);
    }

    public async Task<Result<string>> DeleteAsync(Guid cityId)
    {
        var existingCity = await _cityRepository.GetByGuid(cityId);
        if (!existingCity.IsSuccess)
            return Result<string>.Failure(existingCity.Error);

        return await _cityRepository.Delete(existingCity.Value);
    }
}
