using IntercityTaxi.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace IntercityTaxi.API.Contracts.User;

public class PhoneNumberValidator
{
    private static readonly string PhoneNumberPattern = @"^\+7-(\d{3})-(\d{3})-(\d{2})-(\d{2})$";
    public static Result<bool> IsValidPhoneNumber(string phoneNumber)
    {
        return Result<bool>.Success(Regex.IsMatch(phoneNumber, PhoneNumberPattern));
    }

    public static Result<string> ConvertToStandardFormat(string phoneNumber)
    {
        if (!Regex.IsMatch(phoneNumber, PhoneNumberPattern))
        {
            return Result<string>.Failure("Invalid phone number format.");
        }

        // Удаляем все символы, кроме цифр
        var digitsOnly = Regex.Replace(phoneNumber, @"\D", "");
        return Result<string>.Success(digitsOnly); // возвращаем номер в формате 77777777777
    }
}
