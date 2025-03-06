using IntercityTaxi.Application.DTOs.User;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;

namespace IntercityTaxi.Application.Abstractions
{
    public interface IUserService
    {
        Task<Result<string>> ChangePasswordAsync(string phoneNumber, string newPassword);
        Task<Result<List<User>>> GetAllUsers();
        Task<User?> GetByGuid(Guid userId);
        Task<Result<ResponseUserLogin>> Login(string mobileNumber, string password);
        Task<Result<ResponseUserLogin>> RefreshToken(string AccessToken, string RefreshToken);
        Task<Result<Client>> RegisterClientAsync(string phoneNumber, string password);
    }
}