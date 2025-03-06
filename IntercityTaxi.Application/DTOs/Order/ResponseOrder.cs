using IntercityTaxi.Application.DTOs.City;
using IntercityTaxi.Application.DTOs.User;
using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;
using IntercityTaxi.Domain.Models.Order;

namespace IntercityTaxi.Application.DTOs.Order;

public class ResponseOrder
{
    public Guid Id { get; set; }
    public UserDTO CreatedBy { get; set; } = null!;
    public ResponseCity FromCity { get; set; } = null!;
    public string? FromAddress { get; set; }
    public ResponseCity ToCity { get; set; } = null!;
    public string? ToAddress { get; set; }
    public DateTime Date { get; set; }
    public TripType TripType { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Comment { get; set; }
    public UserRole CreatedByRole { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
