using ChatApp.DTOs;
using ChatApp.Models;

namespace ChatApp.Services;

public interface IUserService
{
    UserDto? RegisterUser(string email, string firstName, string lastName);
    User? GetUser(string userId);
    User? GetUserByEmail(string email);
    IEnumerable<UserDto> GetOnlineUsers();
    bool SetUserOnline(string userId, string connectionId);
    bool SetUserOffline(string connectionId);
    User? GetUserByConnectionId(string connectionId);
}
