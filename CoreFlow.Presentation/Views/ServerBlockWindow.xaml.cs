namespace CoreFlow.Presentation.Views;

public partial class ServerBlockWindow : Window
{
    public ServerBlockWindow()
    {
        InitializeComponent();
        UpdateIcon();

        DataContextChanged += (_, _) =>
        {
            if (DataContext is ServerBlockWindowViewModel serverBlockWindowViewModel)
            {
                serverBlockWindowViewModel.RequestClose += result =>
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