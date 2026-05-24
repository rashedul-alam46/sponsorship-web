public static class AppStateNotifier
{
    public static event Action? OnChange;

    public static void Notify()
    {
        OnChange?.Invoke();
    }
}