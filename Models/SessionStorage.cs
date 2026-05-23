namespace Sponsorship.Models;

public static class SessionStorage
{
    private static SignInResponseDto? _currentUser;

    public static void SetUser(SignInResponseDto user)
    {
        _currentUser = user;
    }

    public static SignInResponseDto? GetUser()
    {
        return _currentUser;
    }

    public static void Clear()
    {
        _currentUser = null;
    }

    public static bool IsLoggedIn => _currentUser != null;
}
