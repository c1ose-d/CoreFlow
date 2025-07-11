namespace CoreFlow.Presentation.ViewModels;

public partial class LoginWindow : Window
{
    public LoginWindow(LoginWindowViewModel loginWindowViewModel)
    {
        InitializeComponent();
        UpdateIcon();

        DataContext = loginWindowViewModel;
    }

    private void UpdateIcon()
    {
        bool light = ThemeHelper.IsSystemLight();
        Color color = light ? Color.FromArgb(228, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255);

        Icon = IconHelper.MakeGlyphIcon("\uEF90", color);
    }
}