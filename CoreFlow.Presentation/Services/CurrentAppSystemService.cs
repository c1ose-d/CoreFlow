namespace CoreFlow.Presentation.Services;

public class CurrentAppSystemService : ICurrentAppSystemService
{
    private readonly string _configFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    private AppSystemDto? _currentAppSystem;

    public event EventHandler? CurrentAppSystemChanged;

    public void SetCurrentAppSystem(AppSystemDto appSystem)
    {
        _currentAppSystem = appSystem;

        try
        {
            string json = File.ReadAllText(_configFilePath);
            dynamic jObj = JObject.Parse(json);
            jObj["AppSystem"] = appSystem.Id;
            File.WriteAllText(_configFilePath, jObj.ToString());
        }
        catch { }

        CurrentAppSystemChanged?.Invoke(this, EventArgs.Empty);
    }

    public AppSystemDto? GetCurrentAppSystem()
    {
        return _currentAppSystem;
    }
}