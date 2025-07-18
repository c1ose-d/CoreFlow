namespace CoreFlow.Presentation.ViewModels;

public partial class ConfirmationWindowViewModel(string body, string title) : ObservableObject
{
    private readonly TaskCompletionSource<bool> _taskCompletionSource = new();
    public Task<bool> Result => _taskCompletionSource.Task;

    public string WindowBody => body;
    public string WindowTitle => title;

    public event Action<bool>? RequestClose;

    [RelayCommand]
    public void Confirm()
    {
        _ = _taskCompletionSource.TrySetResult(true);
        RequestClose?.Invoke(true);
    }

    [RelayCommand]
    public void Cancel()
    {
        _ = _taskCompletionSource.TrySetResult(false);
        RequestClose?.Invoke(false);
    }
}