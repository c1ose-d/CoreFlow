namespace CoreFlow.Presentation.ViewModels;

public partial class UsersPageViewModel(IUserService userService, IAppSystemService appSystemService, INotificationService notificationService, IConfirmationDialogService confirmationDialogService) : ObservableObject, IPageLoadedAware
{
    private readonly IUserService _userService = userService;
    private readonly IAppSystemService _appSystemService = appSystemService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IConfirmationDialogService _confirmationDialogService = confirmationDialogService;

    [ObservableProperty]
    private UserDto? _selectedKey;

    [ObservableProperty]
    private string? _searchString;

    [ObservableProperty]
    private ObservableCollection<UserDto> _userDto = [];

    public async Task Loaded()
    {
        UserDto = new ObservableCollection<UserDto>(await _userService.GetAllAsync());
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        try
        {
            IReadOnlyCollection<UserDto> results = await _userService.SearchAsync(SearchString ?? string.Empty);
            UserDto.Clear();
            foreach (UserDto dto in results)
            {
                UserDto.Add(dto);
            }
        }
        catch (Exception exception)
        {
            _notificationService.Show("Поиск", exception.Message, NotificationType.Caution);
        }
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        UserWindowViewModel userWindowViewModel = new(_userService, _appSystemService, null, false, _notificationService);
        UserWindow userWindow = new()
        {
            DataContext = userWindowViewModel,
            Owner = System.Windows.Application.Current.MainWindow
        };

        if (userWindow.ShowDialog() == true)
        {
            _notificationService.Show("Добавление", "Успешно добавлено", NotificationType.Success);
            UserDto.Clear();
            UserDto = new ObservableCollection<UserDto>(await _userService.GetAllAsync());
        }
    }

    [RelayCommand]
    private async Task UpdateAsync()
    {
        UserWindowViewModel userWindowViewModel = new(_userService, _appSystemService, SelectedKey!, true, _notificationService);
        UserWindow userWindow = new()
        {
            DataContext = userWindowViewModel,
            Owner = System.Windows.Application.Current.MainWindow
        };

        if (userWindow.ShowDialog() == true)
        {
            _notificationService.Show("Изменение", "Успешно обновлено", NotificationType.Success);
            UserDto.Clear();
            UserDto = new ObservableCollection<UserDto>(await _userService.GetAllAsync());
        }
        else
        {
            _notificationService.Show("Изменение", "Ничего не изменено", NotificationType.Attention);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (await _confirmationDialogService.Confirm($"Вы уверены, что хотите удалить {SelectedKey!.FullName}?", "Подтвердите удаление"))
        {
            try
            {
                await _userService.DeleteAsync(SelectedKey.Id);
                _notificationService.Show("Удаление", "Успешно удалено", NotificationType.Success);
                UserDto.Clear();
                UserDto = new ObservableCollection<UserDto>(await _userService.GetAllAsync());
            }
            catch (Exception exception)
            {
                _notificationService.Show("Удаление", exception.Message, NotificationType.Critical);
            }
        }
        else
        {
            return;
        }
    }
}