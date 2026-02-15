namespace ChatApp.Models;

public class User
{
    public string? UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public string? ConnectionId { get; set; }
}
