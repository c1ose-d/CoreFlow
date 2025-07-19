namespace CoreFlow.Presentation.ViewModels;

public partial class TitleBarViewModel : ObservableObject
{
    private readonly IMainWindowService _mainWindowService;
    private readonly ILoginWindowService _loginWindowService;
    private readonly IThemeService _themeService;
    private readonly ICurrentAppSystemService _currentAppSystemService;

    private readonly string _configFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    public string MaxRestoreIcon => IsMaximized ? "" : "";

    public TitleBarViewModel(IAppSystemService appSystemService, IMainWindowService mainWindowService, ILoginWindowService loginWindowService, IThemeService themeService, ICurrentAppSystemService currentAppSystemService)
    {
        _mainWindowService = mainWindowService;
        _loginWindowService = loginWindowService;
        _themeService = themeService;
        _currentAppSystemService = currentAppSystemService;

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
    private AppSystemDto? _selectedItem;

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

        if (onLoaded == true)
        {
            Guid? appSystemId = null;
            try
            {
                string json = File.ReadAllText(_configFilePath);
                dynamic jObj = JObject.Parse(json);
                appSystemId = jObj["AppSystem"];
                File.WriteAllText(_configFilePath, jObj.ToString());
            }
            catch { }

            SelectedItem = SystemDto.FirstOrDefault(predicate => predicate.Id == appSystemId);
        }
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

    partial void OnSelectedItemChanged(AppSystemDto? value)
    {
        if (value != null)
        {
            _currentAppSystemService.SetCurrentAppSystem(value);
        }
    }
}