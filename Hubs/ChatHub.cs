using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs;

public class ChatHub : Hub
{
    private readonly IUserService _userService;

    public ChatHub(IUserService userService)
    {
        _userService = userService;
    }

    public async Task ConnectUser(string userId)
    {
        var user = _userService.GetUser(userId);
        if (user != null)
        {
            _userService.SetUserOnline(userId, Context.ConnectionId);
                
            // Notify all clients about updated online users
            var onlineUsers = _userService.GetOnlineUsers();
            await Clients.All.SendAsync("UpdateOnlineUsers", onlineUsers);
        }
    }

    public async Task SendMessage(string toUserId, string message)
    {
        var fromUser = _userService.GetUserByConnectionId(Context.ConnectionId);
        
        var toUser = _userService.GetUser(toUserId);
            
        if (toUser != null && !string.IsNullOrEmpty(toUser.ConnectionId))
        {
            // Send message to specific user
            await Clients.Client(toUser.ConnectionId).SendAsync("ReceiveMessage", 
                fromUser?.UserId?.ToString(), 
                fromUser?.FirstName + " " + fromUser?.LastName, 
                message,
                DateTime.UtcNow);

            // Send confirmation back to sender
            await Clients.Caller.SendAsync("MessageSent", 
                toUser?.UserId?.ToString(), 
                message,
                DateTime.UtcNow);
        }
        
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _userService.SetUserOffline(Context.ConnectionId);
        
        // Notify all clients about updated online users
        var onlineUsers = _userService.GetOnlineUsers();
        await Clients.All.SendAsync("UpdateOnlineUsers", onlineUsers);
        
        await base.OnDisconnectedAsync(exception);
    }
}
