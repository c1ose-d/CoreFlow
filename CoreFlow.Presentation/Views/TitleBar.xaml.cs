namespace CoreFlow.Presentation.Views;

public partial class TitleBar : UserControl
{
    public TitleBar(TitleBarViewModel titleBarViewModel)
    {
        InitializeComponent();

        DataContext = titleBarViewModel;
    }
}