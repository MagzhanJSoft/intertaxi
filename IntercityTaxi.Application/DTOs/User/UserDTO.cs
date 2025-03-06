using IntercityTaxi.Domain.Interfaces;
using IntercityTaxi.Domain.Models;
using System.Data;

namespace IntercityTaxi.Application.DTOs.User;

public class UserDTO
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }

    private UserDTO(Guid id, string phoneNumber, string fullName)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        FullName = fullName;        
    }

    public static Result<UserDTO> Create(Guid id, string phoneNumber, string fullName)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result<UserDTO>.Failure("Phone number is requery");

        var formattedNumber = FormatPhoneNumber(phoneNumber);
        if (!formattedNumber.IsSuccess)
            return Result<UserDTO>.Failure(formattedNumber.Error);
        

        return Result<UserDTO>.Success(new UserDTO(id, formattedNumber.Value, fullName));
    }

    public static Result<string> FormatPhoneNumber(string phoneNumber)
    {
        if (phoneNumber.Length == 11 && phoneNumber.StartsWith("7"))
        {
            return Result<string>.Success($"+{phoneNumber[0]}-{phoneNumber.Substring(1, 3)}-{phoneNumber.Substring(4, 3)}-{phoneNumber.Substring(7, 2)}-{phoneNumber.Substring(9, 2)}");
        }
        return Result<string>.Failure("Invalid format phonenumber"); // Если формат неверный, возвращаем как есть
    }
}
