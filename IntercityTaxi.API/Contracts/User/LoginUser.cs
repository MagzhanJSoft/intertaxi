namespace IntercityTaxi.API.Contracts.User;

public record LoginUser
    (
        string PhoneNumber,
        string Password
    )

{
    public bool AreEmptyPassword()
    {
        return !string.IsNullOrWhiteSpace(Password);
    }
}
