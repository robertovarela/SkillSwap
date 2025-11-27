using Microsoft.JSInterop;

namespace RDS.Server.Services;

public class ThemeService
{
    private readonly IJSRuntime _jsRuntime;
    private bool _isDarkMode = true;
    private bool _isInitialized;

    public event Action? OnThemeChanged;

    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            if (_isDarkMode != value)
            {
                _isDarkMode = value;
                OnThemeChanged?.Invoke();
            }
        }
    }

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        try
        {
            _isDarkMode = await _jsRuntime.InvokeAsync<bool>("themeStorage.getThemePreference");
            _isInitialized = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inicializar tema: {ex.Message}");
            _isDarkMode = true;
            _isInitialized = true;
        }
    }

    public async Task ToggleThemeAsync()
    {
        IsDarkMode = !IsDarkMode;
        await SaveThemePreferenceAsync();
    }

    public async Task SetThemeAsync(bool isDarkMode)
    {
        if (_isDarkMode == isDarkMode) return;

        IsDarkMode = isDarkMode;
        await SaveThemePreferenceAsync();
    }

    private async Task SaveThemePreferenceAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("themeStorage.saveThemePreference", _isDarkMode);
            Console.WriteLine($"Tema salvo: {(_isDarkMode ? "Dark" : "Light")}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar tema: {ex.Message}");
        }
    }
}

