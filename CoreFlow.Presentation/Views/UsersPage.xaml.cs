namespace CoreFlow.Presentation.Views;

public partial class UsersPage : Page
{
    public UsersPage(UsersPageViewModel usersPageViewModel)
    {
        InitializeComponent();

        DataContext = usersPageViewModel;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is IPageLoadedAware pageLoadedAware)
        {
            _ = pageLoadedAware.Loaded();
        }
    }
}