namespace CoreFlow.Presentation.ViewModels;

public partial class AppSystemsPageViewModel(IAppSystemService appSystemService, INotificationService notificationService, IConfirmationDialogService confirmationDialogService) : ObservableObject, IPageLoadedAware
{
    private readonly IAppSystemService _appSystemService = appSystemService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IConfirmationDialogService _confirmationDialogService = confirmationDialogService;

    [ObservableProperty]
    private AppSystemDto? _selectedKey;

    [ObservableProperty]
    private string? _searchString;

    [ObservableProperty]
    private ObservableCollection<AppSystemDto> _appSystemDto = [];

    public async Task Loaded()
    {
        AppSystemDto = new ObservableCollection<AppSystemDto>(await _appSystemService.GetAllAsync());
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        try
        {
            IReadOnlyCollection<AppSystemDto> results = await _appSystemService.SearchAsync(SearchString ?? string.Empty);
            AppSystemDto.Clear();
            foreach (AppSystemDto dto in results)
            {
                AppSystemDto.Add(dto);
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
        AppSystemWindowViewModel appSystemWindowViewModel = new(_appSystemService, null, false, _notificationService);
        AppSystemWindow appSystemWindow = new()
        {
            DataContext = appSystemWindowViewModel,
            Owner = System.Windows.Application.Current.MainWindow
        };

        if (appSystemWindow.ShowDialog() == true)
        {
            _notificationService.Show("База данных", "Успешно добавлено", NotificationType.Success);
            AppSystemDto.Clear();
            AppSystemDto = new ObservableCollection<AppSystemDto>(await _appSystemService.GetAllAsync());
        }
    }

    [RelayCommand]
    private async Task UpdateAsync()
    {
        AppSystemWindowViewModel appSystemWindowViewModel = new(_appSystemService, SelectedKey!, true, _notificationService);
        AppSystemWindow appSystemWindow = new()
        {
            DataContext = appSystemWindowViewModel,
            Owner = System.Windows.Application.Current.MainWindow
        };

        if (appSystemWindow.ShowDialog() == true)
        {
            _notificationService.Show("База данных", "Успешно обновлено", NotificationType.Success);
            AppSystemDto.Clear();
            AppSystemDto = new ObservableCollection<AppSystemDto>(await _appSystemService.GetAllAsync());
        }
        else
        {
            _notificationService.Show("База данных", "Ничего не изменено", NotificationType.Attention);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (await _confirmationDialogService.Confirm($"Вы уверены, что хотите удалить {SelectedKey!.ShortName}?", "Подтвердите удаление"))
        {
            try
            {
                await _appSystemService.DeleteAsync(SelectedKey.Id);
                _notificationService.Show("База данных", "Успешно удалено", NotificationType.Success);
                AppSystemDto.Clear();
                AppSystemDto = new ObservableCollection<AppSystemDto>(await _appSystemService.GetAllAsync());
            }
            catch (Exception exception)
            {
                _notificationService.Show("База данных", exception.Message, NotificationType.Critical);
            }
        }
        else
        {
            return;
        }
    }
}