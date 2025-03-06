using IntercityTaxi.Application.Abstractions;
using IntercityTaxi.Application.DTOs.City;
using IntercityTaxi.Application.DTOs.Order;
using IntercityTaxi.Application.DTOs.User;
using IntercityTaxi.Application.Repositories;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;
using IntercityTaxi.Domain.Models.Order;

namespace IntercityTaxi.Application.Service;

public class OrderService(
    IUserService userService,
    ICityRepository cityRepository,
    ITripTypeRepository tripTypeRepository,
    IOrderRepository orderRepository
    )
{
    private readonly IUserService _userService = userService;
    private readonly ICityRepository _cityRepository = cityRepository;
    private readonly ITripTypeRepository _tripTypeRepository = tripTypeRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Result<Guid>> CreateOrderAsync(Guid createdById, RequestOrder requestOrder)
    {
        var existingUser = await _userService.GetByGuid(createdById);
        if (existingUser == null)
            return Result<Guid>.Failure("User not found.");

        var existingActiveOrder = await GetActivatedOrder(existingUser);
        if (!existingActiveOrder.IsSuccess)
            return Result<Guid>.Failure(existingActiveOrder.Error);

        var fromCity = await GetCityByGuid(requestOrder.FromCityId);
        if (!fromCity.IsSuccess)
            return Result<Guid>.Failure("From city not found.");

        var toCity = await GetCityByGuid(requestOrder.ToCityId);
        if (!toCity.IsSuccess)
            return Result<Guid>.Failure("To city not found.");

        var tripType = await GetTripTypeByGuid(requestOrder.TripTypeId);
        if (!tripType.IsSuccess)
            return Result<Guid>.Failure(tripType.Error);

        var orderResult = Order.Create(
            existingUser, 
            fromCity.Value, 
            toCity.Value,
            DateTime.UtcNow.AddDays(1),
            tripType.Value, 
            requestOrder.Price, 
            requestOrder.FromAddress,
            requestOrder.ToAddress,
            requestOrder.Comment);

        if (!orderResult.IsSuccess)
            return Result<Guid>.Failure(orderResult.Error);

        return await _orderRepository.AddAsync(orderResult.Value);
                
    }
    public async Task<Result<ResponseOrder>> GetById(Guid id)
    {
        var existingOrder = await _orderRepository.GetByGuid(id);
        if (!existingOrder.IsSuccess)
            return Result<ResponseOrder>.Failure(existingOrder.Error);

        Order order = existingOrder.Value;

        var userDTOResult = UserDTO.Create(
            order.CreatedBy.Id,
            order.CreatedBy.PhoneNumber,
            order.CreatedBy.FullName
            );

        if (!userDTOResult.IsSuccess)
            return Result<ResponseOrder>.Failure(userDTOResult.Error);

        ResponseCity fromCity = new() { Id = order.FromCity.Id, Name = order.FromCity.Name };
        ResponseCity toCity = new() { Id = order.FromCity.Id, Name = order.FromCity.Name };

        ResponseOrder response = new()
        {
            Id = order.Id,
            CreatedBy = userDTOResult.Value,
            FromCity = fromCity,
            FromAddress = order.FromAddress,
            ToCity = toCity,
            ToAddress = order.ToAddress,
            Date = order.Date,
            TripType = order.TripType,
            Price = order.Price,
            Comment = order.Comment,
            CreatedByRole = order.CreatedByRole,
            CreatedAt = order.CreatedAt
        };
        return Result<ResponseOrder>.Success(response);
    }
    public async Task<Result<List<Order>>> Get(Guid userId, Guid userRoleId)
    {
        return await _orderRepository.GetAllOrders(userRoleId);
    }
    
    public async Task<Result<string>> CancelById(Guid userId, Guid id)
    {
        var existingOrder = await _orderRepository.GetByGuid(id);
        if (!existingOrder.IsSuccess)
            return Result<string>.Failure(existingOrder.Error);


        if (!existingOrder.Value.CheckUserId(userId))
            return Result<string>.Failure("UserId does not match");

        existingOrder.Value.Cancel();

        return await _orderRepository.Cancel(existingOrder.Value);
    }

    async Task<Result<bool>> GetActivatedOrder(User user)
    {
        var existingActiveOrder = await _orderRepository.GetMyOrders(user.Id, user.RoleId);
        if (!existingActiveOrder.IsSuccess)
            return Result<bool>.Failure(existingActiveOrder.Error);

        List<Order> orders = existingActiveOrder.Value;

        if (orders.Count > 0 && orders.Any(c => c.IsActivated))
            return Result<bool>.Failure("There is an active order");

        return Result<bool>.Success(false);
    }
    async Task<Result<City>> GetCityByGuid(Guid cityId)
    {
        return await _cityRepository.GetByGuid(cityId);
    }
    async Task<Result<TripType>> GetTripTypeByGuid(Guid tripTypeId)
    {
        return await _tripTypeRepository.GetByGuid(tripTypeId);
    }


    //public async Task<Result<ResponseUserLogin>> Login(string mobileNumber, string password)
    //{
    //    var existingUser = await GetByPhoneNumber(mobileNumber);
    //    if (existingUser == null || !_passwordHasher.Verify(password, existingUser.Password))
    //        return Result<ResponseUserLogin>.Failure("Invalid credentials.");

    //    return await UpdateRefreshTokenAsync(existingUser);
    //}
}
