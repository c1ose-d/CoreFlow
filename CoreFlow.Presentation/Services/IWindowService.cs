namespace CoreFlow.Presentation.Services;

public interface IWindowService
{
    bool IsWindowMaximized { get; }
    event Action? StateChanged;

    void ToggleTheme();
    void Minimize();
    void Maximize();
    void Restore();
    void Close();
}