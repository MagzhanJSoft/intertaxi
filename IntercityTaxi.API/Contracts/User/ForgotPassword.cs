namespace IntercityTaxi.API.Contracts.User;

public record ForgotPassword
    (
        string PhoneNumber,
        string PasswordBase,
        string PasswordConfirm
    )

{
    public bool ArePasswordsMatching()
    {
        return PasswordBase == PasswordConfirm;
    }

    public bool AreEmptyPassword()
    {
        return !string.IsNullOrWhiteSpace(PasswordBase);
    }
}
