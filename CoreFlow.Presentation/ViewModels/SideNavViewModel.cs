namespace CoreFlow.Presentation.ViewModels;

public partial class SideNavViewModel(INavigationService navigationService) : ObservableObject
{
    private readonly INavigationService _navigationService = navigationService;

    [ObservableProperty]
    private double _navigationWidth = 320;

    [ObservableProperty]
    private Style _navigationStyle = (Style)System.Windows.Application.Current.FindResource("NavItem.IconBefore");

    [ObservableProperty]
    private string? _selectedMenuKey;

    [ObservableProperty]
    private string? _selectedBottomMenuKey;

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