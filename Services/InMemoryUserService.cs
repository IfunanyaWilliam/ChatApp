using ChatApp.DTOs;
using ChatApp.Models;
using System.Collections.Concurrent;

namespace ChatApp.Services;

public class InMemoryUserService : IUserService
{
    private readonly ConcurrentDictionary<string, User> _users = new();

    public UserDto? RegisterUser(string email, string firstName, string lastName)
    {
        // Check if user already exists
        var existingUser = _users.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (existingUser != null)
        {
            return null;
        }

        var user = new User
        {
            UserId = Guid.NewGuid().ToString(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IsOnline = false
        };

        _users.TryAdd(user.UserId, user);
        return new UserDto 
        {
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public User? GetUser(string userId)
    {
        _users.TryGetValue(userId, out var user);
        return user;
    }

    public User? GetUserByEmail(string email)
    {
        return _users.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<UserDto> GetOnlineUsers()
    {
        return _users.Values.Where(u => u.IsOnline)
                            .Select(u => new UserDto
                            {
                                UserId = u.UserId,
                                Email = u.Email,
                                FirstName = u.FirstName,
                                LastName = u.LastName
                            }).ToList();
    }

    public bool SetUserOnline(string userId, string connectionId)
    {
        if (_users.TryGetValue(userId, out var user))
        {
            user.IsOnline = true;
            user.ConnectionId = connectionId;
            return true;
        }
        return false;
    }

    public bool SetUserOffline(string connectionId)
    {
        var user = _users.Values.FirstOrDefault(u => u.ConnectionId == connectionId);
        if (user != null)
        {
            user.IsOnline = false;
            user.ConnectionId = null;
            return true;
        }
        return false;
    }

    public User? GetUserByConnectionId(string connectionId)
    {
        return _users.Values.FirstOrDefault(u => u.ConnectionId == connectionId);
    }

    public string GetServerState()
    {
        var number = Random.Shared.Next(1, 5);

        var serverStates = new List<ServerState>()
        {
            new ServerState
            {
                Id = Guid.NewGuid().ToString(),
                Severity = "Normal",
                Node = 1,
                Description = "Server is running smoothly."
            },
            new ServerState
            {
                Id = Guid.NewGuid().ToString(),
                Severity = "Off",
                Node = 2,
                Description = "Server is off."
            },
            new ServerState
            {
                Id = Guid.NewGuid().ToString(),
                Severity = "Critical",
                Node = 3,
                Description = "Server is running in critical State. Attention is needed urgently."
            },
            new ServerState
            {
                Id = Guid.NewGuid().ToString(),
                Severity = "Overloaded",
                Node = 4,
                Description = "Server shutdown due to unandled request."
            }
        };

        var serverState = serverStates.FirstOrDefault(n => n.Node == number);
        return serverState != null ? System.Text.Json.JsonSerializer.Serialize(serverState) : string.Empty;
    }
}
