# Login Module Implementation

## Overview

A complete login module has been implemented for the Sponsorship Web application. Users are now required to authenticate before accessing the system.

## Components Created

### 1. **AccountAuthService** (`Services/AccountAuthService.cs`)

- Handles API communication with the `/api/accountauth/sign-in` endpoint
- Manages login requests and responses
- Provides token storage capability for future enhancements
- Includes error handling for network failures and invalid credentials

### 2. **Login Page** (`Pages/Login.razor`)

- Main login UI with email and password input fields
- Form validation using DataAnnotations
- Loading state indicator during sign-in
- Error message display
- Routes to `/login` and `/` (root path)
- Session storage for user data after successful login
- Responsive design that works on mobile and desktop

### 3. **Home Page** (`Pages/Home.razor`)

- Protected home page that redirects to login if user is not authenticated
- Displays welcome message with user's name
- Navigation cards to different sections of the application
- Route: `/home`

### 4. **Register Page** (`Pages/Register.razor`)

- Placeholder registration page
- Link to sign in for existing users
- Can be implemented with actual registration logic later

### 5. **Styling**

- **Login.razor.css**: Modern gradient-based login UI with smooth transitions
- **Home.razor.css**: Clean home page styling with card-based layout

## Flow

### Login Flow

1. User visits the application (root URL `/`)
2. Login component is displayed
3. User enters email and password
4. On submit, `AccountAuthService.SignInAsync()` is called
5. If successful:
   - User data and tokens are stored in session
   - User is redirected to `/home`
6. If failed:
   - Error message is displayed
   - User remains on login page

### Authentication Check

- Home page checks if user is logged in on initialization
- If not logged in, user is redirected to login page
- This pattern should be applied to all protected pages

## Data Models

The following DTOs are used (already defined in `Models/AccountAuthDto.cs`):

```csharp
// Login Request
public class SignInDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

// Login Response
public class SignInResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Guid? UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int RoleId { get; set; }
}
```

## API Integration

### Endpoint

- **URL**: `/api/accountauth/sign-in`
- **Method**: POST
- **Request**: `SignInDto` (email and password)
- **Response**: `ApiAuthResponse<SignInResponseDto>`

### Sample Request

```json
{
  "email": "user@example.com",
  "password": "password123"
}
```

### Sample Response

```json
{
  "success": true,
  "message": "Sign in successful",
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
    "expiresAt": "2026-05-24T12:00:00Z",
    "userId": "3ccbfb80-52b6-4af0-9816-6be514ab750c",
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "roleId": 4
  }
}
```

## Files Modified/Created

### New Files

- `Services/AccountAuthService.cs`
- `Pages/Login.razor`
- `Pages/Login.razor.css`
- `Pages/Home.razor.css`
- `Pages/Register.razor`

### Modified Files

- `Program.cs` - Added AccountAuthService registration
- `Pages/Home.razor` - Updated with authentication check and welcome message
- `_Imports.razor` - Added necessary using statements

## Configuration

The service uses the `ApiSettings:BaseUrl` from `appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5181/api"
  }
}
```

## Usage

1. **Register the Service** (already done in Program.cs):

   ```csharp
   builder.Services.AddScoped<AccountAuthService>();
   ```

2. **Inject into Components**:

   ```razor
   @inject AccountAuthService AuthService
   @inject NavigationManager Navigation
   ```

3. **Call Login Method**:
   ```csharp
   var result = await AuthService.SignInAsync(signInModel);
   if (result.Success)
   {
       // Handle successful login
   }
   ```

## Future Enhancements

### 1. **Persistent Token Storage**

- Replace session storage with browser localStorage using JS Interop
- Implement token refresh mechanism for expired tokens

### 2. **Authentication State Provider**

- Create custom `AuthenticationStateProvider` for Blazor authentication
- Integrate with Authorization policies
- Add `@attribute [Authorize]` to protected pages

### 3. **Remember Me Feature**

- Add checkbox to save credentials for future logins
- Use secure storage mechanisms

### 4. **Logout Functionality**

- Add logout button to navigation menu
- Clear stored tokens and redirect to login

### 5. **Password Recovery**

- Implement forgot password flow
- Email verification process

### 6. **User Profile Management**

- Edit profile page
- Change password functionality

### 7. **Error Logging**

- Log authentication failures
- Monitor suspicious activities

### 8. **Two-Factor Authentication (2FA)**

- Add TOTP or SMS verification
- Recovery codes for account access

## Testing

### Test Credentials

```
Email: req@test.com
Password: (as per your backend)
```

### Test Flow

1. Navigate to `http://localhost:5000` or `http://localhost:5000/login`
2. Enter test credentials
3. Click "Sign In"
4. If successful, you should be redirected to `/home` with welcome message
5. Try navigating to `/home` without logging in first - should redirect to login

## Security Considerations

1. ⚠️ **Session Storage**: Current implementation uses in-memory session storage. For production, use secure browser storage (localStorage with encryption) or server-side session management.

2. ⚠️ **HTTPS**: Always use HTTPS in production to secure token transmission.

3. ⚠️ **Token Handling**: Implement proper token expiration and refresh logic.

4. ⚠️ **CORS**: Ensure your backend API has proper CORS configuration for the Blazor app.

5. ⚠️ **Input Validation**: All inputs are validated on the client side. Ensure backend also validates.

## Troubleshooting

### Login Button Not Working

- Check browser console for network errors
- Verify API endpoint is correct in `appsettings.json`
- Ensure backend API is running and accessible

### CORS Issues

- Configure backend to allow requests from your Blazor app URL
- Check Network tab in browser DevTools for CORS errors

### Session Data Lost on Page Refresh

- This is expected with in-memory session storage
- Implement localStorage or server-side session management

### Routes Not Found

- Ensure all .razor files are in the `Pages` folder
- Check `@page` directives are correctly formatted

## Architecture Diagram

```
User Browser
    ↓
Login.razor (UI)
    ↓
AccountAuthService (API Call)
    ↓
Backend API (/api/accountauth/sign-in)
    ↓
SignInResponseDto (Response)
    ↓
SessionStorage (User Data)
    ↓
Navigation → Home.razor
```
