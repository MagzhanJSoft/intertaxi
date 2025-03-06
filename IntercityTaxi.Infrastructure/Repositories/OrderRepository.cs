using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;
using IntercityTaxi.Domain.Models.Order;
using IntercityTaxi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IntercityTaxi.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    //Create
    public async Task<Result<Guid>> AddAsync(Order order)
    {
        try
            {
                _context.Entry(order.CreatedBy).State = EntityState.Unchanged;
                _context.Entry(order.TripType).State = EntityState.Unchanged;
                _context.Entry(order.FromCity).State = EntityState.Unchanged;
                _context.Entry(order.ToCity).State = EntityState.Unchanged;
                await _context.Orders.AddAsync(order);            
                await _context.SaveChangesAsync();
                return Result<Guid>.Success(order.Id);

            }
        catch (DbUpdateException ex)
            {
                return Result<Guid>.Failure($"DB error: {ex.InnerException?.Message ?? ex.Message}");
            }
        catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        
    }

    // get order by id
    public async Task<Result<Order>> GetByGuid(Guid orderId)
    {
        try
        {
            var order = await _context.Orders
                    .AsNoTracking()
                    .Include(cr => cr.CreatedBy)
                    .Include(f => f.FromCity)
                    .Include(t => t.ToCity)
                    .Include(tr => tr.TripType)
                    .Include(r => r.CreatedByRole)
                    .FirstOrDefaultAsync(u => u.Id == orderId
                    && u.IsActivated);
            if (order == null)
                return Result<Order>.Failure("Order is not found");

            return Result<Order>.Success(order);
        }
        catch (Exception ex)
        {
            return Result<Order>.Failure(ex.Message);
        }
        
    }

    public async Task<Result<List<Order>>> GetAllOrders(Guid userRoleId)
    {
        try
        {
            return Result<List<Order>>.Success(await _context.Orders
                .AsNoTracking()
                .Include(cr => cr.CreatedBy)
                .Include(f => f.FromCity)
                .Include(t => t.ToCity)
                .Include(tr => tr.TripType)
                .Include(r => r.CreatedByRole)
                .Where(c => c.CreatedByRoleId != userRoleId
                     && c.ExpirationDate >= DateTime.UtcNow
                     && c.IsActivated)
                .OrderByDescending(d=>d.ExpirationDate)
                .ToListAsync());
        }
        catch (Exception ex)
        {
            return Result<List<Order>>.Failure(ex.Message);
        }
    }
    public async Task<Result<List<Order>>> GetMyOrders(Guid userId, Guid userRoleId)
    {
        try
        {
            return Result<List<Order>>.Success(await _context.Orders
                .AsNoTracking()
                .Include(f => f.FromCity)
                .Include(t => t.ToCity)
                .Include(tr => tr.TripType)
                .Where(c => c.CreatedByRoleId == userRoleId
                     && c.CreatedById == userId
                     && !c.IsDeleted)
                .OrderByDescending(d => d.IsActivated)
                .ToListAsync());
        }
        catch (Exception ex)
        {
            return Result<List<Order>>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> Cancel(Order order)
    {
        try
        {
            _context.Orders.Update(order);
            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            return Result<string>.Success("Successful updated."); ;
        }
        catch (DbUpdateException ex)
        {
            return Result<string>.Failure("Ошибка при обновлении базы данных: " + ex.Message);
        }
    }
}
