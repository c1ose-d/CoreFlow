namespace CoreFlow.Presentation.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsPageViewModel settingsPageViewModel)
    {
        InitializeComponent();

        DataContext = settingsPageViewModel;
    }
}