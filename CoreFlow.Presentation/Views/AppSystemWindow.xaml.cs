namespace CoreFlow.Presentation.Views;

public partial class AppSystemWindow : Window
{
    public AppSystemWindow()
    {
        InitializeComponent();
        UpdateIcon();

        DataContextChanged += (_, _) =>
        {
            if (DataContext is AppSystemWindowViewModel appSystemWindowViewModel)
            {
                appSystemWindowViewModel.RequestClose += result =>
                {
                    DialogResult = result;
                };
            }
        };
    }

    private void UpdateIcon()
    {
        bool light = ThemeHelper.IsSystemLight();
        Color color = light ? Color.FromArgb(228, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255);

        Icon = IconHelper.MakeGlyphIcon("\uEF90", color);
    }
}