namespace ChatApp.Models
{
    public class ServerState
    {
        public string Id { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public int Node { get; set; } 
        public string Description { get; set; } = string.Empty;
    }
}
