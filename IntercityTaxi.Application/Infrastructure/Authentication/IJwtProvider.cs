using IntercityTaxi.Domain.Interfaces;
using System.Security.Claims;

namespace IntercityTaxi.Application.Infrastructure.Authentication
{
    public interface IJwtProvider
    {
        string GenerateRefreshToken();
        string GenerateToken(Guid userId, Guid userRoleId);
        Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string userId);
    }
}