namespace CoreFlow.Presentation.Views;

public partial class SideNav : UserControl
{
    public SideNav(SideNavViewModel sideNavViewModel)
    {
        InitializeComponent();

        DataContext = sideNavViewModel;
    }
}