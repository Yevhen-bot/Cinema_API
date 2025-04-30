namespace Cinema_API.Jwt
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiresHours { get; set; }
    }
}
