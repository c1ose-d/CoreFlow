namespace CoreFlow.Presentation.ViewModels;

public partial class ServerWindowViewModel : ObservableObject, IWindowLoadedAware
{
    private readonly IServerService _serverService;
    private readonly IServerBlockService _serverBlockService;
    private readonly INotificationService _notificationService;
    private readonly ICurrentAppSystemService _currentAppSystemService;

    private readonly HashSet<string> _dirty = [];

    private readonly bool _isEdit;

    public event Action<bool>? RequestClose;

    private readonly Guid? _originalItemId;

    [ObservableProperty]
    private string _windowTitle;

    [ObservableProperty]
    private ServerDto? _serverDto;

    [ObservableProperty]
    private string? _ipAdress;

    [ObservableProperty]
    private string? _hostName;

    [ObservableProperty]
    private string? _userName;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private ServerBlockDto? _selectedItem;

    [ObservableProperty]
    private ObservableCollection<ServerBlockDto> _items = [];

    public ServerWindowViewModel(IServerService serverService, IServerBlockService serverBlockService, ServerDto? serverDto, bool isEdit, INotificationService notificationService, ICurrentAppSystemService currentAppSystemService)
    {
        _serverService = serverService;
        _serverBlockService = serverBlockService;
        _notificationService = notificationService;
        _currentAppSystemService = currentAppSystemService;
        _isEdit = isEdit;
        _windowTitle = isEdit ? "Редактировать" : "Добавить";
        _serverDto = serverDto;

        if (_serverDto != null)
        {
            _ipAdress = _serverDto.IpAddress;
            _hostName = _serverDto.HostName;
            _userName = _serverDto.UserName;
            _originalItemId = _serverDto.ServerBlock.Id;
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
        IReadOnlyCollection<ServerBlockDto> all = await _serverBlockService.GetByAppSystemIdAsync(_currentAppSystemService.GetCurrentAppSystem()!.Id);
        Items = new ObservableCollection<ServerBlockDto>(all);

        if (ServerDto != null)
        {
            SelectedItem = Items.FirstOrDefault(predicate => predicate.Id == _originalItemId);
        }
    }

    public bool WasSaved { get; private set; }

    [RelayCommand]
    public async Task Save()
    {
        string? ipAddress = _dirty.Contains(nameof(IpAdress)) ? IpAdress : null;
        string? hostName = _dirty.Contains(nameof(HostName)) ? HostName : null;
        string? userName = _dirty.Contains(nameof(UserName)) ? UserName : null;
        string? password = _dirty.Contains(nameof(Password)) ? Password : null;
        ServerBlockDto? serverBlock = _dirty.Contains(nameof(SelectedItem)) ? SelectedItem : null;

        try
        {
            if (_isEdit)
            {
                if (_dirty.Count > 0)
                {
                    _ = await _serverService.UpdateAsync(new UpdateServerDto(ServerDto!.Id, ipAddress, hostName, userName, password, serverBlock));
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
                _ = await _serverService.CreateAsync(new CreateServerDto(ipAddress!, hostName, userName!, password!, serverBlock!));
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