using CoreFlow.Application.DTOs.AppSystem;

namespace CoreFlow.Presentation.ViewModels;

public partial class TitleBarViewModel : ObservableObject
{
    private readonly IMainWindowService _mainWindowService;
    private readonly ILoginWindowService _loginWindowService;
    private readonly IThemeService _themeService;

    public string MaxRestoreIcon => IsMaximized ? "" : "";

    public TitleBarViewModel(IMainWindowService mainWindowService, ILoginWindowService loginWindowService, IThemeService themeService)
    {
        _mainWindowService = mainWindowService;
        _loginWindowService = loginWindowService;
        _themeService = themeService;

        _mainWindowService.StateChanged += () => IsMaximized = _mainWindowService.IsWindowMaximized;
        IsMaximized = _mainWindowService.IsWindowMaximized;
        UserVisibility = UserDto != null ? Visibility.Visible : Visibility.Collapsed;

        _mainWindowService.Loaded += async () => await User(true);
    }

    [ObservableProperty]
    public UserDto? _userDto;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MaxRestoreIcon))]
    private bool _isMaximized;

    [ObservableProperty]
    private Visibility _userVisibility = Visibility.Collapsed;

    [ObservableProperty]
    private string? _fullName;

    [ObservableProperty]
    private ObservableCollection<AppSystemDto> _systemDto = [];

    [RelayCommand]
    private async Task User(bool? onLoaded = false)
    {
        UserDto = await _loginWindowService.ShowDialogAsync(onLoaded);
        if (UserDto is null)
        {
            return;
        }

        FullName = UserDto.FullName;
        SystemDto = new ObservableCollection<AppSystemDto>(UserDto.AppSystems);
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        _themeService.ToggleTheme();
    }

    [RelayCommand]
    private void Minimize()
    {
        _mainWindowService.Minimize();
    }

    [RelayCommand]
    private void MaximizeRestore()
    {
        if (IsMaximized)
        {
            _mainWindowService.Restore();
        }
        else
        {
            _mainWindowService.Maximize();
        }
    }

    [RelayCommand]
    private void Close()
    {
        _mainWindowService.Close();
    }

    partial void OnUserDtoChanged(UserDto? value)
    {
        UserVisibility = value is not null ? Visibility.Visible : Visibility.Collapsed;
    }
}