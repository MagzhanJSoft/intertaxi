namespace IntercityTaxi.API.Contracts.User;

public record RefreshTokenModel
(
    string AccessToken,
    string RefreshToken
)
{
    public bool AreEmptyTokens()
    {
        return !string.IsNullOrWhiteSpace(AccessToken) || !string.IsNullOrWhiteSpace(RefreshToken);
    }
}
