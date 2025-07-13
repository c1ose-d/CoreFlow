namespace CoreFlow.Presentation.Services;

public class ConfirmationDialogService : IConfirmationDialogService
{
    public async Task<bool> Confirm(string body, string title)
    {
        ConfirmationWindowViewModel confirmationWindowViewModel = new(body, title);
        ConfirmationWindow confirmationWindow = new()
        {
            DataContext = confirmationWindowViewModel,
            Owner = System.Windows.Application.Current.MainWindow
        };
        _ = confirmationWindow.ShowDialog();
        return await confirmationWindowViewModel.Result;
    }
}