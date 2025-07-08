namespace CoreFlow.Presentation.Services;

public class WindowService : IWindowService
{
    private readonly Window _window;

    public WindowService(Window window)
    {
        _window = window;
        _window.StateChanged += (_, _) => StateChanged?.Invoke();
    }

    public bool IsWindowMaximized => _window.WindowState == WindowState.Maximized;

    public event Action? StateChanged;

    public void ToggleTheme()
    {

    }

    public void Minimize()
    {
        SystemCommands.MinimizeWindow(_window);
    }

    public void Maximize()
    {
        SystemCommands.MaximizeWindow(_window);
    }

    public void Restore()
    {
        SystemCommands.RestoreWindow(_window);
    }

    public void Close()
    {
        SystemCommands.CloseWindow(_window);
    }
}