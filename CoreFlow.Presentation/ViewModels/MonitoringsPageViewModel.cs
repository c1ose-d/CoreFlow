namespace CoreFlow.Presentation.ViewModels;

public partial class MonitoringsPageViewModel(INavigationService navigationService) : ObservableObject
{
    private readonly INavigationService _navigationService = navigationService;

    [ObservableProperty]
    private string? _selectedMenuKey;

    partial void OnSelectedMenuKeyChanged(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        _navigationService.NavigateTo(value);
    }
}