namespace CoreFlow.Presentation.Services;

public class LoginWindowService(IServiceProvider serviceProvider, IUserService userService, IAppSystemService appSystemService, INotificationService notificationService, ICurrentUserService currentUserService, ICurrentAppSystemService currentAppSystemService) : ILoginWindowService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IUserService _userService = userService;
    private readonly IAppSystemService _appSystemService = appSystemService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ICurrentAppSystemService _currentAppSystemService = currentAppSystemService;

    private readonly string _configFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    public async Task<UserDto?> ShowDialogAsync(bool? onLoaded = false)
    {
        IServiceScope serviceScope = _serviceProvider.CreateScope();
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        Guid? userId = null;
        try
        {
            string json = File.ReadAllText(_configFilePath);
            dynamic jObj = JObject.Parse(json);
            userId = jObj["User"];
            File.WriteAllText(_configFilePath, jObj.ToString());
        }
        catch (Exception exception)
        {
            _notificationService.Show("Login", exception.Message, NotificationType.Critical);
        }

        UserDto? user;
        if (userId != null)
        {
            user = await _userService.GetByIdAsync((Guid)userId);
            _currentUserService.SetCurrentUser(user!);
        }

        if (onLoaded != true)
        {
            LoginWindow loginWindow = serviceProvider.GetRequiredService<LoginWindow>();
            LoginWindowViewModel loginWindowViewModel = (LoginWindowViewModel)loginWindow.DataContext;

            bool? dialogResult = loginWindow.ShowDialog();
            if (dialogResult == true)
            {
                user = loginWindowViewModel.UserDto;

                if (user != null)
                {
                    try
                    {
                        string json = File.ReadAllText(_configFilePath);
                        dynamic jObj = JObject.Parse(json);
                        jObj["User"] = user.Id;
                        File.WriteAllText(_configFilePath, jObj.ToString());
                        _currentUserService.SetCurrentUser(user);
                    }
                    catch (Exception exception)
                    {
                        _notificationService.Show("Login", exception.Message, NotificationType.Critical);
                    }
                }
            }
        }

        serviceScope.Dispose();
        return await Task.FromResult(_currentUserService.GetCurrentUser());
    }
}