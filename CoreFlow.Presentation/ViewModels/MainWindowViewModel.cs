namespace CoreFlow.Presentation.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public ObservableCollection<NotificationMessage> NotificationMessages { get; }

    private readonly INotificationService _notificationService;

    public MainWindowViewModel(INotificationService notificationService)
    {
        _notificationService = notificationService;
        NotificationMessages = _notificationService.Messages;
    }
}