using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models.Order;

namespace IntercityTaxi.Application.Repositories
{
    public interface IOrderRepository
    {
        Task<Result<Guid>> AddAsync(Order order);
        Task<Result<string>> Cancel(Order order);
        Task<Result<List<Order>>> GetAllOrders(Guid userRoleId);
        Task<Result<Order>> GetByGuid(Guid orderId);
        Task<Result<List<Order>>> GetMyOrders(Guid userId, Guid userRoleId);
    }
}