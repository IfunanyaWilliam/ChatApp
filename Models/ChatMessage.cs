namespace ChatApp.Models;

public class ChatMessage
{
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
