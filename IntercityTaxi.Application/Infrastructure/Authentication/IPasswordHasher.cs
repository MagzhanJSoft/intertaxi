namespace IntercityTaxi.Application.Infrastructure.Authentication;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string HashedPassword);
}