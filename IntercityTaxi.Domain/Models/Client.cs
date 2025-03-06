using IntercityTaxi.Domain.Interfaces;

namespace IntercityTaxi.Domain.Models;

public class Client : User
{
    public float Rating { get; private set; }

    private Client() { }
    private Client(string phoneNumber, string fullName, string hashedPassword, string refreshToken) : base(phoneNumber, fullName, hashedPassword, refreshToken)
    {
        Rating = 3.0f; // Начальный рейтинг
    }

    public static Result<Client> RegisterClient(string phoneNumber, string fullName, string hashedPassword, string refreshToken)
    {
        var userResult = User.Register(phoneNumber, fullName, hashedPassword, refreshToken);
        if (!userResult.IsSuccess)
        {
            return Result<Client>.Failure(userResult.Error);
        }

        var client = new Client(phoneNumber, fullName, hashedPassword, refreshToken);
        return Result<Client>.Success(client);
    }

    public Result<float> UpdateRating(float newRating)
    {
        if (newRating is < 1.0f or > 5.0f)
        {
            return Result<float>.Failure("Рейтинг должен быть от 1.0 до 5.0");
        }

        Rating = newRating;
        return Result<float>.Success(Rating);
    }
}
