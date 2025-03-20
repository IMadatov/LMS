namespace Auth.Domain.DTOs;

public class JWTTokenModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
