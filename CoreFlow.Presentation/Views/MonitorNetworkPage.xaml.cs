namespace CoreFlow.Presentation.Views;

public partial class MonitorNetworkPage : Page
{
    public MonitorNetworkPage(MonitorNetworkPageViewModel monitorNetworkPageViewModel)
    {
        InitializeComponent();

        DataContext = monitorNetworkPageViewModel;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is IPageLoadedAware pageLoadedAware)
        {
            _ = pageLoadedAware.Loaded();
        }
    }
}