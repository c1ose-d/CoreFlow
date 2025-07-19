namespace CoreFlow.Presentation.ViewModels;

public partial class ServersPageViewModel : ObservableObject, IPageLoadedAware
{
    private readonly IServerBlockService _serverBlockService;
    private readonly IServerService _serverService;
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICurrentAppSystemService _currentAppSystemService;
    private readonly INotificationService _notificationService;
    private readonly IConfirmationDialogService _confirmationDialogService;

    [ObservableProperty]
    private object? _selectedKey;

    [ObservableProperty]
    private string? _searchString;

    [ObservableProperty]
    private ObservableCollection<ServerBlockDto> _serverBlockDto = [];

    public ServersPageViewModel(IServerBlockService serverBlockService, IServerService serverService, IUserService userService, ICurrentUserService currentUserService, ICurrentAppSystemService currentAppSystemService, INotificationService notificationService, IConfirmationDialogService confirmationDialogService)
    {
        _serverBlockService = serverBlockService;
        _serverService = serverService;
        _userService = userService;
        _currentUserService = currentUserService;
        _currentAppSystemService = currentAppSystemService;
        _notificationService = notificationService;
        _confirmationDialogService = confirmationDialogService;

        currentAppSystemService.CurrentAppSystemChanged += async (_, _) =>
        {
            await Loaded();
        };
    }

    public async Task Loaded()
    {
        ServerBlockDto = new ObservableCollection<ServerBlockDto>(await _serverBlockService.GetByAppSystemIdAsync(_currentAppSystemService.GetCurrentAppSystem()!.Id));
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        try
        {
            IReadOnlyCollection<ServerBlockDto> results = await _serverBlockService.SearchAsync(SearchString ?? string.Empty);
            ServerBlockDto.Clear();
            foreach (ServerBlockDto dto in results)
            {
                ServerBlockDto.Add(dto);
            }
        }
        catch (Exception exception)
        {
            _notificationService.Show("Поиск", exception.Message, NotificationType.Caution);
        }
    }

    [RelayCommand]
    private async Task CreateBlockAsync()
    {
        ServerBlockWindowViewModel serverBlockWindowViewModel = new(_serverBlockService, _userService, null, false, _notificationService, _currentUserService, _currentAppSystemService);
        ServerBlockWindow serverBlockWindow = new()
        {
            DataContext = serverBlockWindowViewModel,
            Owner = System.Windows.Application.Current.MainWindow
        };

        if (serverBlockWindow.ShowDialog() == true)
        {
            _notificationService.Show("Добавление", "Успешно добавлено", NotificationType.Success);
            await Loaded();
        }
    }

    [RelayCommand]
    private async Task CreateServerAsync()
    {
        ServerWindowViewModel serverWindowViewModel = new(_serverService, _serverBlockService, null, false, _notificationService, _currentAppSystemService);
        ServerWindow serverWindow = new()
        {
            DataContext = serverWindowViewModel,
            Owner = System.Windows.Application.Current.MainWindow
        };

        if (serverWindow.ShowDialog() == true)
        {
            _notificationService.Show("Добавление", "Успешно добавлено", NotificationType.Success);
            await Loaded();
        }
    }

    [RelayCommand]
    private async Task UpdateAsync()
    {
        if (SelectedKey is ServerBlockDto serverBlockDto)
        {
            ServerBlockWindowViewModel serverBlockWindowViewModel = new(_serverBlockService, _userService, serverBlockDto, true, _notificationService, _currentUserService, _currentAppSystemService);
            ServerBlockWindow serverBlockWindow = new()
            {
                DataContext = serverBlockWindowViewModel,
                Owner = System.Windows.Application.Current.MainWindow
            };

            if (serverBlockWindow.ShowDialog() == true)
            {
                _notificationService.Show("Изменение", "Успешно обновлено", NotificationType.Success);
                await Loaded();
            }
        }
        else if (SelectedKey is ServerDto serverDto)
        {
            ServerWindowViewModel serverWindowViewModel = new(_serverService, _serverBlockService, serverDto, true, _notificationService, _currentAppSystemService);
            ServerWindow serverWindow = new()
            {
                DataContext = serverWindowViewModel,
                Owner = System.Windows.Application.Current.MainWindow
            };

            if (serverWindow.ShowDialog() == true)
            {
                _notificationService.Show("Изменение", "Успешно обновлено", NotificationType.Success);
                await Loaded();
            }
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        try
        {
            if (SelectedKey is ServerBlockDto serverBlockDto)
            {
                if (await _confirmationDialogService.Confirm($"Вы уверены, что хотите удалить {serverBlockDto.Name}?", "Подтвердите удаление"))
                {
                    await _serverBlockService.DeleteAsync(serverBlockDto.Id);
                    _notificationService.Show("Удаление", "Успешно удалено", NotificationType.Success);
                    await Loaded();
                }
                else
                {
                    return;
                }
            }
            else if (SelectedKey is ServerDto serverDto)
            {
                if (await _confirmationDialogService.Confirm($"Вы уверены, что хотите удалить {serverDto.DisplayName}?", "Подтвердите удаление"))
                {
                    await _serverService.DeleteAsync(serverDto.Id);
                    _notificationService.Show("Удаление", "Успешно удалено", NotificationType.Success);
                    await Loaded();
                }
                else
                {
                    return;
                }
            }
        }
        catch (Exception exception)
        {
            _notificationService.Show("База данных", exception.Message, NotificationType.Critical);
        }
    }
}