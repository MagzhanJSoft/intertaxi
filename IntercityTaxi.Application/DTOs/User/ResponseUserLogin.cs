namespace IntercityTaxi.Application.DTOs.User
{
    public class ResponseUserLogin
    {
        public bool IsLogedIn { get; set; } = false;
        public string AuthToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
