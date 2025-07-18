namespace CoreFlow.Presentation.ViewModels;

public partial class SettingsPageViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ICurrentUserService _currentUserService;

    [ObservableProperty]
    private string? _selectedMenuKey;

    [ObservableProperty]
    private Visibility _adminPanelVisibility = Visibility.Collapsed;

    public SettingsPageViewModel(INavigationService navigationService, ICurrentUserService currentUserService)
    {
        _navigationService = navigationService;
        _currentUserService = currentUserService;

        UserDto? user = _currentUserService.GetCurrentUser();

        if (user != null)
        {
            AdminPanelVisibility = user.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        }

        _currentUserService.CurrentUserChanged += (_, _) =>
        {
            UserDto? user = _currentUserService.GetCurrentUser();

            if (user != null)
            {
                AdminPanelVisibility = user.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            }
        };
    }

    partial void OnSelectedMenuKeyChanged(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        _navigationService.NavigateTo(value);
    }
}