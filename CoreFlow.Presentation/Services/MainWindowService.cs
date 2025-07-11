namespace CoreFlow.Presentation.Services;

public class MainWindowService : IMainWindowService
{
    private Window? _window;

    public event Action? StateChanged;
    public event Action? Loaded;

    public bool IsWindowMaximized => _window?.WindowState == WindowState.Maximized;

    public void Initialize(Window window)
    {
        _window = window;
        _window.StateChanged += (_, _) => StateChanged?.Invoke();
        _window.Loaded += (_, _) => Loaded?.Invoke();
        StateChanged?.Invoke();
    }

    public void Minimize()
    {
        SystemCommands.MinimizeWindow(_window!);
    }

    public void Maximize()
    {
        SystemCommands.MaximizeWindow(_window!);
    }

    public void Restore()
    {
        SystemCommands.RestoreWindow(_window!);
    }

    public void Close()
    {
        SystemCommands.CloseWindow(_window!);
    }
}