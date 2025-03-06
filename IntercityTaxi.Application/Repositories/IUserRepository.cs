using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;

namespace IntercityTaxi.Application.Repositories;

public interface IUserRepository
{
    Task<Result<string>> AddAsync(User user);
    Task<Result<List<User>>> GetAllUsers();
    Task<User?> GetByGuid(Guid userId);
    Task<User?> GetByPhoneNumber(string mobileNumber);
    Task<Result<string>> UpdateAsync(User user);
}
