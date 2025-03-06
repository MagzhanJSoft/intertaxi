using BCrypt.Net;
using IntercityTaxi.Application.Infrastructure;
using IntercityTaxi.Application.Infrastructure.Authentication;

namespace IntercityTaxi.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string HashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, HashedPassword);
    }


}
