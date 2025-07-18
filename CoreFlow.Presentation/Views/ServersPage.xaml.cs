namespace CoreFlow.Presentation.Views;

public partial class ServersPage : Page
{
    public ServersPage(ServersPageViewModel serversPageViewModel)
    {
        InitializeComponent();

        DataContext = serversPageViewModel;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is IPageLoadedAware pageLoadedAware)
        {
            _ = pageLoadedAware.Loaded();
        }
    }
}