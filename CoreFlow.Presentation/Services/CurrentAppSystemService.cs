namespace CoreFlow.Presentation.Services;

public class CurrentAppSystemService(INotificationService notificationService) : ICurrentAppSystemService
{
    private readonly INotificationService _notificationService = notificationService;

    private readonly string _configFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    private AppSystemDto? _currentAppSystem;

    public event EventHandler? CurrentAppSystemChanged;

    public void SetCurrentAppSystem(AppSystemDto appSystemDto)
    {
        _currentAppSystem = appSystemDto;

        try
        {
            string json = File.ReadAllText(_configFilePath);
            dynamic jObj = JObject.Parse(json);
            jObj["AppSystem"] = appSystemDto.Id;
            File.WriteAllText(_configFilePath, jObj.ToString());
        }
        catch (Exception exception)
        {
            _notificationService.Show("WPF", exception.Message, NotificationType.Critical);
        }

        CurrentAppSystemChanged?.Invoke(this, EventArgs.Empty);
    }

    public AppSystemDto? GetCurrentAppSystem()
    {
        return _currentAppSystem;
    }
}