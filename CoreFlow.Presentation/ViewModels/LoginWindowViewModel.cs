namespace CoreFlow.Presentation.ViewModels;

public partial class LoginWindowViewModel(IUserService userService, INotificationService notificationService) : ObservableObject
{
    private readonly IUserService _userService = userService;
    private readonly INotificationService _notificationService = notificationService;

    [ObservableProperty]
    private string _userName = "";

    [ObservableProperty]
    private string _password = "";

    public UserDto? UserDto { get; private set; }

    [RelayCommand]
    private async Task LoginAsync(Window window)
    {
        try
        {
            UserDto result = await _userService.GetByUserNamePasswordAsync(UserName, Password);

            UserDto = result;

            _notificationService.Show("Авторизация", $"Пользователь {UserDto.FullName} успешно авторизовался.", NotificationType.Success);
            window.DialogResult = true;
        }
        catch (Exception exception)
        {
            _notificationService.Show("Авторизация", $"{exception.Message}", NotificationType.Critical);
        }
    }

    [RelayCommand]
    private static void Cancel(Window window)
    {
        window.DialogResult = false;
    }
}