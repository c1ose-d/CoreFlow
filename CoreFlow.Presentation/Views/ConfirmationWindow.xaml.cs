namespace CoreFlow.Presentation.Views;

public partial class ConfirmationWindow : Window
{
    public ConfirmationWindow()
    {
        InitializeComponent();
        UpdateIcon();

        DataContextChanged += (_, _) =>
        {
            if (DataContext is ConfirmationWindowViewModel confirmationWindowViewModel)
            {
                confirmationWindowViewModel.RequestClose += result =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        DialogResult = result;
                    });
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