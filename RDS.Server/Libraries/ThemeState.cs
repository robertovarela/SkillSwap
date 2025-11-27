namespace RDS.Server.Libraries;

public class ThemeState
{
    public bool IsDarkTheme { get; private set; }

    public event Action? OnThemeChanged;

    public void SetDarkTheme(bool isDark)
    {
        IsDarkTheme = isDark;
        OnThemeChanged?.Invoke();
    }
}