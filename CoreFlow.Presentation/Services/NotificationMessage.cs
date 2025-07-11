namespace CoreFlow.Presentation.Services;

public enum NotificationType
{
    Success,
    Caution,
    Critical,
    Attention
}

public class NotificationMessage(string title, string body, NotificationType notificationType = NotificationType.Attention)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Title { get; } = title;
    public string Body { get; } = body;
    public NotificationType NotificationType { get; } = notificationType;
}