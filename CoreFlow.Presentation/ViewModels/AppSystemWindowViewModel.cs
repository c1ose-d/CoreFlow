using CoreFlow.Application.DTOs.AppSystem;

namespace CoreFlow.Presentation.ViewModels;

public partial class AppSystemWindowViewModel : ObservableObject
{
    private readonly IAppSystemService _appSystemService;
    private readonly INotificationService _notificationService;

    private readonly HashSet<string> _dirty = [];

    private readonly bool _isEdit;

    public event Action<bool>? RequestClose;

    [ObservableProperty]
    private string _windowTitle;

    [ObservableProperty]
    public AppSystemDto? _appSystemDto;

    [ObservableProperty]
    public string? _name;

    [ObservableProperty]
    public string? _shortName;

    public AppSystemWindowViewModel(IAppSystemService appSystemService, AppSystemDto? appSystemDto, bool isEdit, INotificationService notificationService)
    {
        _appSystemService = appSystemService;
        _notificationService = notificationService;
        _isEdit = isEdit;
        _windowTitle = isEdit ? "Редактировать" : "Добавить";
        _appSystemDto = appSystemDto;

        if (_appSystemDto != null)
        {
            _name = _appSystemDto.Name;
            _shortName = _appSystemDto.ShortName;
        }

        PropertyChanged += (s, e) =>
        {
            if (e.PropertyName is not null and not (nameof(_dirty)))
            {
                _ = _dirty.Add(e.PropertyName);
            }
        };
    }

    public bool WasSaved { get; private set; }

    [RelayCommand]
    public async Task Save()
    {
        string? name = _dirty.Contains(nameof(Name)) ? Name : null;
        string? shortName = _dirty.Contains(nameof(ShortName)) ? ShortName : null;

        try
        {
            if (_isEdit)
            {
                if (_dirty.Count > 0)
                {
                    _ = await _appSystemService.UpdateAsync(new UpdateAppSystemDto(AppSystemDto!.Id, name, shortName));
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
                _ = await _appSystemService.CreateAsync(new CreateAppSystemDto(name!, shortName!));
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