namespace CoreFlow.Presentation.Views;

public partial class MonitoringsPage : Page
{
    public MonitoringsPage(MonitoringsPageViewModel monitoringsPageViewModel)
    {
        InitializeComponent();

        DataContext = monitoringsPageViewModel;
    }
}