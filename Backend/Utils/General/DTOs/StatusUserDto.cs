namespace General.DTOs;

public class StatusUserDto
{
    public string Id { get; set; } = string.Empty;

    public bool IsOnTelegramBotActive { get; set; }

    public bool HasPhotoProfile { get; set; }
}
