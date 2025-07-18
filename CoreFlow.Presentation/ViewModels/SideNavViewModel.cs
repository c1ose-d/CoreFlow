namespace CoreFlow.Presentation.ViewModels;

public partial class SideNavViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ICurrentUserService _currentUserService;

    [ObservableProperty]
    private double _navigationWidth = 320;

    [ObservableProperty]
    private Style _navigationStyle = (Style)System.Windows.Application.Current.FindResource("NavItem.IconBefore");

    [ObservableProperty]
    private string? _selectedMenuKey;

    [ObservableProperty]
    private string? _selectedBottomMenuKey;

    [ObservableProperty]
    private bool _isMenuEnabled;

    public SideNavViewModel(INavigationService navigationService, ICurrentUserService currentUserService)
    {
        _navigationService = navigationService;
        _currentUserService = currentUserService;

        IsMenuEnabled = _currentUserService.GetCurrentUser() != null;

        _currentUserService.CurrentUserChanged += (_, _) =>
        {
            IsMenuEnabled = _currentUserService.GetCurrentUser() != null;
        };
    }

    [RelayCommand]
    private void ChangeWidth()
    {
        if (NavigationWidth == 320)
        {
            NavigationWidth = 48;
            NavigationStyle = (Style)System.Windows.Application.Current.FindResource("NavItem.IconOnly");
        }
        else
        {
            NavigationWidth = 320;
            NavigationStyle = (Style)System.Windows.Application.Current.FindResource("NavItem.IconBefore");
        }
    }

    partial void OnSelectedMenuKeyChanged(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        SelectedBottomMenuKey = null;

        _navigationService.NavigateTo(value);

        SelectedMenuKey = null;
    }

    partial void OnSelectedBottomMenuKeyChanged(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        SelectedMenuKey = null;

        _navigationService.NavigateTo(value);

        SelectedBottomMenuKey = null;
    }
}