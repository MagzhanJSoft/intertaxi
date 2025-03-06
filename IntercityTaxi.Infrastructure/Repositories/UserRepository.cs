using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;
using IntercityTaxi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace IntercityTaxi.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> AddAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Result<string>.Success("Success addedd");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure("Error adding to database: " + ex.Message);
        }

    }

    public async Task<Result<string>> UpdateAsync(User user)
    {
        try
        {
            _context.Users.Update(user);

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            return Result<string>.Success("Successful updated."); ;
        }
        catch (DbUpdateException ex)
        {
            return Result<string>.Failure("Ошибка при обновлении базы данных: " + ex.Message);
        }
    }

    public async Task<User?> GetByPhoneNumber(string mobileNumber)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.PhoneNumber == mobileNumber);
    }

    public async Task<User?> GetByGuid(Guid userId)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<Result<List<User>>> GetAllUsers()
    {
        try
        {
            return Result<List<User>>.Success(await _context.Users
                .AsNoTracking()
                .Include(r=> r.Role) 
                .ToListAsync());
        }
        catch (Exception ex)
        {
            return Result<List<User>>.Failure(ex.Message);
        }


    }
}
