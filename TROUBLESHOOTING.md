# Troubleshooting: Online Users Not Displaying

## Step 1: Check Browser Console

1. Open the Chat page
2. Press **F12** to open Developer Tools
3. Go to **Console** tab
4. Look for these messages:
   - "SignalR Connected" (should appear)
   - "Received online users: [...]" (should show user list)
   - Any red error messages

## Step 2: Common Issues & Fixes

### Issue 1: LocalStorage Not Set
**Symptoms:** Page redirects to home immediately

**Fix:** 
- Go back to Register page
- Register a new user
- Should redirect to Chat automatically

### Issue 2: Users Not Filtering Correctly
**Symptoms:** Console shows users but they don't display

**Check:** Look at console output for "Filtered users:"
- If empty array `[]` but users exist, userId comparison might be wrong

**Fix:** Clear localStorage and re-register:
```javascript
// In browser console, run:
localStorage.clear()
// Then register again
```

### Issue 3: SignalR Not Connecting
**Symptoms:** Console shows "SignalR Connection Error"

**Fix:**
- Make sure app is running
- Check the URL in browser matches launchSettings.json
- Restart the application

### Issue 4: Multiple Users Same Browser
**Problem:** Testing with multiple tabs in same browser

**Solution:** Use different browsers or incognito/private windows:
- Window 1: Chrome (normal)
- Window 2: Chrome Incognito
- Window 3: Edge or Firefox

## Step 3: Test Sequence

1. **Stop the application** completely
2. **Clear browser data:**
   - Press F12
   - Go to Application tab (Chrome) or Storage tab (Firefox)
   - Clear all LocalStorage
3. **Restart application**
4. **Open Browser 1:**
   - Register User 1 (e.g., alice@test.com)
   - Should see Chat page with "No other users online"
5. **Open Browser 2 (different browser or incognito):**
   - Register User 2 (e.g., bob@test.com)
   - Should see Alice in online users
6. **Check Browser 1:**
   - Should automatically see Bob appear in online users

## Step 4: Check What Console Shows

### Expected Console Output:
```
SignalR Connected
Received online users: [{userId: "xxx", firstName: "Alice", ...}]
Current userId: yyy
Filtered users: []  // Empty if you're the only user
```

OR (when another user connects):
```
Received online users: [{userId: "xxx", firstName: "Alice", ...}, {userId: "yyy", firstName: "Bob", ...}]
Current userId: xxx
Filtered users: [{userId: "yyy", firstName: "Bob", ...}]
```

## Step 5: Manual Test via API

Test if backend is working:

```bash
# Register User 1
curl -X POST https://localhost:5001/api/user/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test1@test.com","firstName":"Test","lastName":"One"}' \
  -k

# Register User 2
curl -X POST https://localhost:5001/api/user/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test2@test.com","firstName":"Test","lastName":"Two"}' \
  -k

# Check online users (should be empty - users not connected via SignalR)
curl https://localhost:5001/api/user/online -k
```

## Step 6: Check the File Changes

Make sure these files match the originals:
- `Views/Home/Chat.cshtml`
- `Hubs/ChatHub.cs`
- `Services/InMemoryUserService.cs`

If you made custom changes, compare with the original files.

## Still Not Working?

Send me a screenshot of:
1. Browser console output
2. The Chat page showing the issue
3. Any error messages
