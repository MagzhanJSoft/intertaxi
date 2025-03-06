using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models.Order;
using IntercityTaxi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IntercityTaxi.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Create(City city)
    {
        try
        {
            await _context.AddAsync(city);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Success addedd");
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DB error: {ex.InnerException?.Message ?? ex.Message}");
            return Result<string>.Failure($"DB error: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }

    public async Task<Result<List<City>>> Get()
    {
        try
        {
            return Result<List<City>>.Success(
                await _context.Cities
                    .AsNoTracking()
                    .ToListAsync()
                    );
        }
        catch (Exception ex) 
        {
            return Result<List<City>>.Failure(ex.Message);
        }

    }

    public async Task<Result<string>> Delete(City city)
    {
        try
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return Result<string>.Success("Success deleted.");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }

    }

    public async Task<Result<City>> GetByGuid(Guid cityId)
    {
        try
        {
            var city = await _context.Cities
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == cityId);

            if (city == null)
                return Result<City>.Failure("City is not found.");

            return Result<City>.Success(city);
        }
        catch (Exception ex)
        {
            return Result<City>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> GetByName(string cityName)
    {
        try
        {
            var city = await _context.Cities
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name.ToUpper() == cityName.ToUpper());

            if (city != null)
                return Result<string>.Failure("The city already exists");

            return Result<string>.Success("Not found");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
}
