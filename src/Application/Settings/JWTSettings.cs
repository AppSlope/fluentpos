namespace FluentPOS.Application.Settings
{
    public class JWTSettings
    {
        public string Key { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public int RefreshTokenExpirationInDays { get; set; }
    }
}