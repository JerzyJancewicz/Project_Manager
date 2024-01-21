namespace ApiService1.DTOs.UserDtos
{
    public class UserLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
