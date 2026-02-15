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
git clone <your-repo-url>
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

## Deploying to Azure

### Setup Azure Resources

1. **Create an Azure Web App**
   - Go to [Azure Portal](https://portal.azure.com)
   - Create a new Web App
   - Choose .NET 8 runtime
   - Note your app name (e.g., `my-chat-app`)

2. **Get Publish Profile**
   - In Azure Portal, go to your Web App
   - Click "Get publish profile" in the Overview section
   - Download the `.PublishSettings` file

3. **Configure GitHub Secrets**
   - Go to your GitHub repository
   - Navigate to Settings → Secrets and variables → Actions
   - Click "New repository secret"
   - Name: `AZURE_WEBAPP_PUBLISH_PROFILE`
   - Value: Paste the entire content of the `.PublishSettings` file
   - Click "Add secret"

4. **Update Workflow File**
   - Edit `.github/workflows/azure-deploy.yml`
   - Change `AZURE_WEBAPP_NAME` to your Azure Web App name
   ```yaml
   env:
     AZURE_WEBAPP_NAME: my-chat-app  # Your Azure Web App name
   ```

5. **Enable WebSockets in Azure**
   - In Azure Portal, go to your Web App
   - Navigate to Configuration → General settings
   - Set "Web sockets" to **On**
   - Click Save

6. **Push to GitHub**
   ```bash
   git add .
   git commit -m "Initial commit"
   git push origin main
   ```

7. **GitHub Actions will automatically:**
   - Build the application
   - Run tests
   - Publish the app
   - Deploy to Azure

8. **Access your app**
   - Visit `william-onah-chat-app-hwf2eadxb8feckdv.spaincentral-01.azurewebsites.net`

### Important Azure Configuration

For SignalR to work properly on Azure, ensure:

1. **WebSockets are enabled** (Configuration → General settings)
2. **Always On is enabled** (Configuration → General settings)
3. **ARR Affinity is ON** (Configuration → General settings)

## Testing the Application

### Test User Registration
```bash
curl -X POST https://localhost:5001/api/user/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","firstName":"Test","lastName":"User"}'
```

### Test Get Online Users
```bash
curl https://localhost:5001/api/user/online
```

## Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Real-time Communication**: SignalR
- **Frontend**: Bootstrap 5, Vanilla JavaScript
- **Storage**: In-Memory (ConcurrentDictionary)
- **Deployment**: GitHub Actions → Azure Web App

## Architecture Notes

- **In-Memory Storage**: Users are stored in-memory using `ConcurrentDictionary` for thread-safety
- **SignalR**: Provides real-time bidirectional communication between server and clients
- **Connection Management**: Users are marked online when they connect to SignalR hub and offline when they disconnect
- **Message Delivery**: Messages are sent directly to connected users using their SignalR connection ID

## Limitations

- Data is stored in-memory and will be lost on app restart
- No message persistence
- No authentication/authorization
- Single server deployment (no scale-out without additional configuration)

## Future Enhancements

- Add database persistence (SQL Server, PostgreSQL)
- Implement authentication (ASP.NET Core Identity)
- Add message history storage
- Support group chats
- Add file sharing
- Implement typing indicators
- Add read receipts
- Configure Redis backplane for multi-server SignalR

## License

MIT License
