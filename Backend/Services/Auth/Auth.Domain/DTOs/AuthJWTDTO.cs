namespace Auth.Domain.DTOs;

public class AuthJWTDTO
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}
