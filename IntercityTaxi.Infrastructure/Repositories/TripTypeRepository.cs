using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models.Order;
using IntercityTaxi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IntercityTaxi.Infrastructure.Repositories;

public class TripTypeRepository : ITripTypeRepository
{
    private readonly AppDbContext _context;

    public TripTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Create(TripType tripType)
    {
        try
        {
            await _context.AddAsync(tripType);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Success addedd");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }

    public async Task<Result<List<TripType>>> Get()
    {
        try
        {
            return Result<List<TripType>>.Success(
                await _context.TripTypes
                    .AsNoTracking()
                    .ToListAsync()
                    );
        }
        catch (Exception ex)
        {
            return Result<List<TripType>>.Failure(ex.Message);
        }

    }

    public async Task<Result<string>> Delete(TripType tripType)
    {
        try
        {
            _context.TripTypes.Remove(tripType);
            await _context.SaveChangesAsync();
            return Result<string>.Success("Success deleted.");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }

    }

    public async Task<Result<TripType>> GetByGuid(Guid tripTypeId)
    {
        try
        {
            var tripType = await _context.TripTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == tripTypeId);

            if (tripType == null)
                return Result<TripType>.Failure("Type of trip is not found.");

            return Result<TripType>.Success(tripType);
        }
        catch (Exception ex)
        {
            return Result<TripType>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> GetByName(string tripTypeName)
    {
        try
        {
            var city = await _context.TripTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name.ToUpper() == tripTypeName.ToUpper());

            if (city != null)
                return Result<string>.Failure("The type of trip already exists");

            return Result<string>.Success("Not found");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
}
