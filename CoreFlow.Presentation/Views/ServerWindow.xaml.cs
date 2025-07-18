namespace CoreFlow.Presentation.Views;

public partial class ServerWindow : Window
{
    public ServerWindow()
    {
        InitializeComponent();
        UpdateIcon();

        DataContextChanged += (_, _) =>
        {
            if (DataContext is ServerWindowViewModel serverWindowViewModel)
            {
                serverWindowViewModel.RequestClose += result =>
                {
                    DialogResult = result;
                };
            }
        };
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is IWindowLoadedAware windowLoadedAware)
        {
            _ = windowLoadedAware.Loaded();
        }
    }

    private void UpdateIcon()
    {
        bool light = ThemeHelper.IsSystemLight();
        Color color = light ? Color.FromArgb(228, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255);

        Icon = IconHelper.MakeGlyphIcon("\uEF90", color);
    }
}