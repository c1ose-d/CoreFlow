namespace CoreFlow.Presentation.ViewModels;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly Window _window;

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _isMaximized;

    public MainWindowViewModel(Window window)
    {
        _window = window;
        _isMaximized = window.WindowState == WindowState.Maximized;

        MinimizeCommand = new RelayCommand(_ => SystemCommands.MinimizeWindow(window));
        MaximizeRestoreCommand = new RelayCommand(_ =>
        {
            if (window.WindowState == WindowState.Normal)
            {
                SystemCommands.MaximizeWindow(_window);
            }
            else
            {
                SystemCommands.RestoreWindow(_window);
            }
        });
        CloseCommand = new RelayCommand(_ => SystemCommands.CloseWindow(window));
    }

    public bool IsMaximized
    {
        get => _isMaximized;
        private set
        {
            if (_isMaximized != value)
            {
                _isMaximized = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxRestoreIcon));
            }
        }
    }

    public string MaxRestoreIcon => IsMaximized ? "" : "";

    public ICommand MinimizeCommand { get; }

    public ICommand MaximizeRestoreCommand { get; }

    public ICommand CloseCommand { get; }

    private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}