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
}
