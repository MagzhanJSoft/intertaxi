using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models.Order;

namespace IntercityTaxi.Application.Repositories
{
    public interface ICityRepository
    {
        Task<Result<string>> Create(City city);
        Task<Result<string>> Delete(City city);
        Task<Result<List<City>>> Get();
        Task<Result<City>> GetByGuid(Guid cityId);
        Task<Result<string>> GetByName(string cityName);
    }
}