namespace CoreFlow.Presentation.ViewModels;

public partial class ServerBlockWindowViewModel : ObservableObject, IWindowLoadedAware
{
    private readonly IServerBlockService _serverBlockService;
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICurrentAppSystemService _currentAppSystemService;

    private readonly HashSet<string> _dirty = [];

    private readonly bool _isEdit;

    public event Action<bool>? RequestClose;

    private readonly Guid? _originalItemId;

    [ObservableProperty]
    private string _windowTitle;

    [ObservableProperty]
    public ServerBlockDto? _serverBlockDto;

    [ObservableProperty]
    public string? _name;

    [ObservableProperty]
    public AppSystemDto? _selectedItem;

    [ObservableProperty]
    public ObservableCollection<AppSystemDto> _items = [];

    public ServerBlockWindowViewModel(IServerBlockService serverBlockService, IUserService userService, ServerBlockDto? serverBlockDto, bool isEdit, INotificationService notificationService, ICurrentUserService currentUserService, ICurrentAppSystemService currentAppSystemService)
    {
        _serverBlockService = serverBlockService;
        _userService = userService;
        _notificationService = notificationService;
        _currentUserService = currentUserService;
        _currentAppSystemService = currentAppSystemService;
        _isEdit = isEdit;
        _windowTitle = isEdit ? "Редактировать" : "Добавить";
        _serverBlockDto = serverBlockDto;

        if (_serverBlockDto != null)
        {
            _name = _serverBlockDto.Name;
            _originalItemId = _serverBlockDto.AppSystemId;
        }

        PropertyChanged += (s, e) =>
        {
            if (e.PropertyName is not null and not (nameof(_dirty)) and not (nameof(Items)))
            {
                _ = _dirty.Add(e.PropertyName);
            }
        };
    }

    public async Task Loaded()
    {
        UserDto userDto = await _userService.GetByIdAsync(_currentUserService.GetCurrentUser()!.Id) ?? throw new KeyNotFoundException();

        IReadOnlyCollection<AppSystemDto> all = userDto.AppSystems;
        Items = new ObservableCollection<AppSystemDto>(all);

        SelectedItem = ServerBlockDto != null ? Items.FirstOrDefault(predicate => predicate.Id == _originalItemId) : Items.FirstOrDefault(predicate => predicate.Id == _currentAppSystemService.GetCurrentAppSystem()!.Id);
    }

    public bool WasSaved { get; private set; }

    [RelayCommand]
    public async Task Save()
    {
        string? name = _dirty.Contains(nameof(Name)) ? Name : null;
        AppSystemDto? appSystem = _dirty.Contains(nameof(SelectedItem)) ? SelectedItem : null;

        try
        {
            if (_isEdit)
            {
                if (_dirty.Count > 0)
                {
                    _ = await _serverBlockService.UpdateAsync(new UpdateServerBlockDto(ServerBlockDto!.Id, name, appSystem!.Id));
                }
                else
                {
                    WasSaved = true;
                    RequestClose?.Invoke(false);
                    return;
                }
            }
            else
            {
                _ = await _serverBlockService.CreateAsync(new CreateServerBlockDto(name!, appSystem!.Id));
            }

            WasSaved = true;
            RequestClose?.Invoke(true);
        }
        catch (Exception exception)
        {
            _notificationService.Show("База данных", exception.Message, NotificationType.Critical);
        }
    }

    [RelayCommand]
    public void Cancel()
    {
        RequestClose?.Invoke(false);
    }
}