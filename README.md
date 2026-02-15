# Chat App - Real-time Messaging with SignalR

A simple MVC .NET 8 application with real-time chat functionality using SignalR.

## Features

1. **User Registration**: Register with email, first name, and last name (stored in-memory)
2. **Online Users Display**: See all currently logged-in users in real-time
3. **Real-time Chat**: Chat with other online users using SignalR

## Project Structure

```
ChatApp/
├── Controllers/
│   ├── HomeController.cs       # MVC controller for views
│   └── UserController.cs       # API controller for user registration
├── Hubs/
│   └── ChatHub.cs              # SignalR hub for real-time communication
├── Models/
│   ├── User.cs                 # User model
│   └── ChatMessage.cs          # Chat message model
├── Services/
│   ├── IUserService.cs         # User service interface
│   └── InMemoryUserService.cs  # In-memory user storage
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml        # Registration page
│   │   └── Chat.cshtml         # Chat interface
│   └── Shared/
│       └── _Layout.cshtml      # Shared layout
├── wwwroot/
│   └── css/
│       └── site.css            # Custom styles
└── Program.cs                  # Application entry point
```

## API Endpoints

### POST /api/user/register
Register a new user

**Request Body:**
```json
{
  "email": "user@example.com",
  "firstName": "John",
  "lastName": "Doe"
}
```

**Response (Success):**
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "email": "user@example.com",
  "firstName": "John",
  "lastName": "Doe"
}
```

### GET /api/user/online
Get all online users

**Response:**
```json
[
  {
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe"
  }
]
```

### GET /api/user/{userId}
Get user by ID

**Response:**
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "email": "user@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "isOnline": true
}
```

## SignalR Hub Methods

### Client → Server

- `ConnectUser(userId)` - Connect a user and mark them as online
- `SendMessage(toUserId, message)` - Send a message to another user

### Server → Client

- `UpdateOnlineUsers(users)` - Broadcast updated list of online users
- `ReceiveMessage(fromUserId, fromUserName, message, timestamp)` - Receive a message from another user
- `MessageSent(toUserId, message, timestamp)` - Confirmation that message was sent

## Running Locally

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 / VS Code / Rider

### Steps

1. Clone the repository
```bash
git clone https://github.com/IfunanyaWilliam/ChatApp.git
cd ChatApp
```

2. Restore dependencies
```bash
dotnet restore
```

3. Run the application
```bash
dotnet run
```

4. Open browser and navigate to `https://localhost:5001` or `http://localhost:5000`

5. Register multiple users (open multiple browser windows/tabs)

6. Start chatting!



## License

MIT License
