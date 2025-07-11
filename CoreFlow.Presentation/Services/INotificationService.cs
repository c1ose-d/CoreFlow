namespace CoreFlow.Presentation.Services;

public interface INotificationService
{
    ObservableCollection<NotificationMessage> Messages { get; }
    void Show(string title, string body, NotificationType type = NotificationType.Attention);
}