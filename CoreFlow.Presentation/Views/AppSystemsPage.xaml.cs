namespace CoreFlow.Presentation.Views;

public partial class AppSystemsPage : Page
{
    public AppSystemsPage(AppSystemsPageViewModel appSystemsPageViewModel)
    {
        InitializeComponent();

        DataContext = appSystemsPageViewModel;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is IPageLoadedAware pageLoadedAware)
        {
            _ = pageLoadedAware.Loaded();
        }
    }
}