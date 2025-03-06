using IntercityTaxi.Application.Abstractions;
using IntercityTaxi.Application.DTOs.User;
using IntercityTaxi.Application.Infrastructure.Authentication;
using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;

namespace IntercityTaxi.Application.Service;

public class UserService(
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository
        ) : IUserService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<ResponseUserLogin>> Login(string mobileNumber, string password)
    {
        var existingUser = await GetByPhoneNumber(mobileNumber);
        if (existingUser == null || !_passwordHasher.Verify(password, existingUser.Password))
            return Result<ResponseUserLogin>.Failure("Invalid credentials.");

        return await UpdateRefreshTokenAsync(existingUser);
    }


    public async Task<Result<Client>> RegisterClientAsync(string phoneNumber, string password)
    {
        var existingUser = await GetByPhoneNumber(phoneNumber);
        if (existingUser != null)
            return Result<Client>.Failure("Пользователь с таким номером уже существует./User already exists.");

        var hashedPassword = _passwordHasher.Generate(password);

        var clientResult = Client.RegisterClient(phoneNumber, "", hashedPassword, "");
        if (!clientResult.IsSuccess)
        {
            return Result<Client>.Failure(clientResult.Error);
        }

        var resultAdd = await _userRepository.AddAsync(clientResult.Value);
        if (!resultAdd.IsSuccess)
            return Result<Client>.Failure(resultAdd.Error);

        return Result<Client>.Success(clientResult.Value);


    }

    public async Task<Result<string>> ChangePasswordAsync(string phoneNumber, string newPassword)
    {
        var user = await GetByPhoneNumber(phoneNumber);
        if (user == null)
            return Result<string>.Failure("User not found.");

        return await UpdatePasswordAsync(user, newPassword);
    }

    public async Task<Result<ResponseUserLogin>> RefreshToken(string AccessToken, string RefreshToken)
    {
        var resultGetMobileNumber = GetPrincipal(AccessToken);
        if (!resultGetMobileNumber.IsSuccess)
            return Result<ResponseUserLogin>.Failure("Пользователь не найден./User not found.");

        var user = await GetByPhoneNumber(resultGetMobileNumber.Value);
        if (user == null)
            return Result<ResponseUserLogin>.Failure("User not found.");

        if (user.RefreshToken != RefreshToken)
            return Result<ResponseUserLogin>.Failure("Invalid credentials.");


        return await UpdateRefreshTokenAsync(user);

    }

    Result<string> GetPrincipal(string accessToken)
    {
        var principalResult = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);
        if (!principalResult.IsSuccess)
            return Result<string>.Failure(principalResult.Error);

        string mobileNumber = principalResult.Value.Claims.FirstOrDefault(c => c.Type == "mobileNumber")!.Value;

        return Result<string>.Success(mobileNumber);
    }

    async Task<Result<ResponseUserLogin>> UpdateRefreshTokenAsync(User user)
    {

        var refreshToken = _jwtProvider.GenerateRefreshToken();
        var accessToken = _jwtProvider.GenerateToken(user.Id, user.RoleId);

        user.UpdateRefreshToken(refreshToken);

        var resultUpdate = await _userRepository.UpdateAsync(user);
        return resultUpdate.IsSuccess ?
            Result<ResponseUserLogin>.Success(new ResponseUserLogin
            {
                IsLogedIn = true,
                RefreshToken = refreshToken,
                AuthToken = accessToken
            })
            : Result<ResponseUserLogin>.Failure(resultUpdate.Error);
    }

    async Task<Result<string>> UpdatePasswordAsync(User user, string newPassword)
    {
        user.ChangePassword(_passwordHasher.Generate(newPassword));

        var resultUpdate = await _userRepository.UpdateAsync(user);
        return resultUpdate.IsSuccess ?
            Result<string>.Success("The password has been changed.") :
            Result<string>.Failure(resultUpdate.Error);
    }

    async Task<User?> GetByPhoneNumber(string phoneNumber)
    {
        return await _userRepository.GetByPhoneNumber(phoneNumber);
    }

    public async Task<User?> GetByGuid(Guid userId)
    {
        return await _userRepository.GetByGuid(userId);
    }

    public async Task<Result<List<User>>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();

    }
}
