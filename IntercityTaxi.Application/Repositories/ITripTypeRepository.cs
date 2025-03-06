using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models.Order;

namespace IntercityTaxi.Application.Repositories
{
    public interface ITripTypeRepository
    {
        Task<Result<string>> Create(TripType tripType);
        Task<Result<string>> Delete(TripType tripType);
        Task<Result<List<TripType>>> Get();
        Task<Result<TripType>> GetByGuid(Guid tripTypeId);
        Task<Result<string>> GetByName(string tripTypeName);
    }
}