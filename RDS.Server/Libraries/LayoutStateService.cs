namespace RDS.Server.Libraries;

public class LayoutStateService
{
    public event Action? OnSidebarToggled;

    private bool _isSidebarVisible = true;
    public bool IsSidebarVisible => _isSidebarVisible;

    public void ToggleSidebar()
    {
        _isSidebarVisible = !_isSidebarVisible;
        OnSidebarToggled?.Invoke();
    }
}
