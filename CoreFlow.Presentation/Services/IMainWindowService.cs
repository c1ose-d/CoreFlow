namespace CoreFlow.Presentation.Services;

public interface IMainWindowService
{
    void Initialize(Window window);

    bool IsWindowMaximized { get; }
    event Action? StateChanged;
    event Action? Loaded;

    void Minimize();
    void Maximize();
    void Restore();
    void Close();
}