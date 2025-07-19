namespace CoreFlow.Presentation.ViewModels;

public partial class UserWindowViewModel : ObservableObject, IWindowLoadedAware
{
    private readonly IUserService _userService;
    private readonly IAppSystemService _appSystemService;
    private readonly INotificationService _notificationService;

    private readonly HashSet<string> _dirty = [];

    private readonly bool _isEdit;

    private readonly UserDto? _userDto;

    private readonly HashSet<Guid> _originalSystemIds = [];

    public event Action<bool>? RequestClose;

    public bool WasSaved { get; private set; }

    [ObservableProperty]
    private string _windowTitle;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _middleName;

    [ObservableProperty]
    private string? _userName;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private bool? _isAdmin = false;

    [ObservableProperty]
    private ObservableCollection<AppSystemDto> _selectedItems = [];

    [ObservableProperty]
    public ObservableCollection<AppSystemDto> _items = [];

    public UserWindowViewModel(IUserService userService, IAppSystemService appSystemService, UserDto? userDto, bool isEdit, INotificationService notificationService)
    {
        _userService = userService;
        _appSystemService = appSystemService;
        _notificationService = notificationService;
        _isEdit = isEdit;
        _windowTitle = isEdit ? "Редактировать" : "Добавить";
        _userDto = userDto;

        if (_userDto != null)
        {
            _lastName = _userDto.LastName;
            _firstName = _userDto.FirstName;
            _middleName = _userDto.MiddleName;
            _userName = _userDto.UserName;
            _isAdmin = _userDto.IsAdmin;
        }
        _originalSystemIds = userDto?.AppSystems.Select(x => x.Id).ToHashSet() ?? [];

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
        IReadOnlyCollection<AppSystemDto> all = await _appSystemService.GetAllAsync();
        Items = new ObservableCollection<AppSystemDto>(all);

        SelectedItems = new ObservableCollection<AppSystemDto>([.. Items.Where(x => _originalSystemIds.Contains(x.Id))]);
        SelectedItems.CollectionChanged += (_, _) =>
        {
            _ = _dirty.Add(nameof(SelectedItems));
        };
    }

    [RelayCommand]
    public async Task Save()
    {
        string? lastName = _dirty.Contains(nameof(LastName)) ? LastName : null;
        string? firstName = _dirty.Contains(nameof(FirstName)) ? FirstName : null;
        string? middleName = _dirty.Contains(nameof(MiddleName)) ? MiddleName : null;
        string? userName = _dirty.Contains(nameof(UserName)) ? UserName : null;
        string? password = _dirty.Contains(nameof(Password)) ? Password : null;
        bool? isAdmin = _dirty.Contains(nameof(IsAdmin)) ? IsAdmin : null;
        List<Guid>? appSystemIds = _dirty.Contains(nameof(SelectedItems)) ? [.. SelectedItems!.Select(x => x.Id)] : null;

        try
        {
            if (_isEdit && _dirty.Count == 0)
            {
                WasSaved = true;
                RequestClose?.Invoke(false);
                return;
            }

            if (_isEdit)
            {
                _ = await _userService.UpdateAsync(new UpdateUserDto(_userDto!.Id, lastName, firstName, middleName, userName, password, isAdmin, appSystemIds));
            }
            else
            {
                List<Guid> systemIds = [.. SelectedItems!.Select(x => x.Id)];
                _ = await _userService.CreateAsync(new CreateUserDto(LastName!, FirstName!, MiddleName, UserName!, Password!, (bool)IsAdmin!, systemIds));
            }

            WasSaved = true;
            RequestClose?.Invoke(true);
        }
        catch (Exception ex)
        {
            _notificationService.Show("База данных", ex.Message, NotificationType.Critical);
        }
    }

    [RelayCommand]
    public void Cancel()
    {
        RequestClose?.Invoke(false);
    }
}