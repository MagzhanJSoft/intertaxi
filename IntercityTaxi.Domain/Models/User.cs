using IntercityTaxi.Domain.Interfaces;

namespace IntercityTaxi.Domain.Models;

public class User
{
    private static readonly string PhoneNumberPattern = @"^7\d{10}$";
    public Guid Id { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public string RefreshToken { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;    
    public Guid RoleId { get; private set; }
    public UserRole Role { get; private set; }

    protected User() { }
    protected User(string phoneNumber, string fullName, string hashedPassword, string refreshToken = "", UserRole role= null)
    {
        Id = Guid.NewGuid();
        PhoneNumber = phoneNumber;
        Password = hashedPassword;
        RefreshToken = refreshToken;
        FullName = fullName;
        Role = role ?? new UserRole("Client");
        RoleId = Role.Id;
    }

    public static Result<User> Register(string phoneNumber, string fullName, string hashedPassword, string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Result<User>.Failure("Phone number is required.");
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, PhoneNumberPattern))
        {
            return Result<User>.Failure("Invalid phone number format.");

        }
        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            return Result<User>.Failure("Password is required.");
        }

        var user = new User(phoneNumber, fullName, hashedPassword, refreshToken);
        return Result<User>.Success(user);
    }

    public void UpdateRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
    public void ChangePassword(string password)
    {
        Password = password;
    }
}
