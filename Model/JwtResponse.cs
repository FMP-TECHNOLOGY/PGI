namespace Model
{
    public class JwtResponse
    {
        public string? Token { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
