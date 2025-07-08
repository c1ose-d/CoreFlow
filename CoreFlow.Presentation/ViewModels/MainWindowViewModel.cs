using CommunityToolkit.Mvvm.Input;
using CoreFlow.Presentation.Services;

namespace CoreFlow.Presentation.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IWindowService _windowService;

    [ObservableProperty]
    private bool isMaximized;

    public string MaxRestoreIcon => IsMaximized ? "" : "";

    public MainWindowViewModel(IWindowService windowService)
    {
        _windowService = windowService;
        IsMaximized = _windowService.IsWindowMaximized;
        _windowService.StateChanged += OnWindowStateChanged;
    }

    private void OnWindowStateChanged()
    {
        IsMaximized = _windowService.IsWindowMaximized;
        OnPropertyChanged(nameof(MaxRestoreIcon));
    }

    [RelayCommand]
    private void Minimize()
    {
        _windowService.Minimize();
    }

    [RelayCommand]
    private void MaximizeRestore()
    {
        if (IsMaximized)
        {
            _windowService.Restore();
        }
        else
        {
            _windowService.Maximize();
        }
    }

    [RelayCommand]
    private void Close()
    {
        _windowService.Close();
    }
}