namespace MovieApp.Api.Models.Jwt
{
    public class JwtSettings
    {
        public string TokenExpiryHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }
}
