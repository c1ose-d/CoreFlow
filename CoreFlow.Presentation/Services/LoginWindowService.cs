namespace CoreFlow.Presentation.Services;

public class LoginWindowService(IServiceProvider serviceProvider, IUserService userService, ICurrentUserService currentUserService) : ILoginWindowService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IUserService _userService = userService;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly string _configFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    public async Task<UserDto?> ShowDialogAsync(bool? onLoaded = false)
    {
        IServiceScope serviceScope = _serviceProvider.CreateScope();
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        Guid? id = null;
        try
        {
            string json = File.ReadAllText(_configFilePath);
            dynamic jObj = JObject.Parse(json);
            id = jObj["User"];
            File.WriteAllText(_configFilePath, jObj.ToString());
        }
        catch { }

        UserDto? result;
        if (id != null)
        {
            result = await _userService.GetByIdAsync((Guid)id);

            if (result != null)
            {
                _currentUserService.SetCurrentUser(result);
            }
        }

        if (onLoaded != true)
        {
            LoginWindow loginWindow = serviceProvider.GetRequiredService<LoginWindow>();
            LoginWindowViewModel loginWindowViewModel = (LoginWindowViewModel)loginWindow.DataContext;

            bool? dialogResult = loginWindow.ShowDialog();
            if (dialogResult == true)
            {
                result = loginWindowViewModel.UserDto;

                if (result != null)
                {
                    _currentUserService.SetCurrentUser(result);
                }

                if (result != null)
                {
                    try
                    {
                        string json = File.ReadAllText(_configFilePath);
                        dynamic jObj = JObject.Parse(json);
                        jObj["User"] = result.Id;
                        File.WriteAllText(_configFilePath, jObj.ToString());
                    }
                    catch { }
                }
            }
        }

        serviceScope.Dispose();
        return await Task.FromResult(_currentUserService.GetCurrentUser());
    }
}