namespace CoreFlow.Presentation.Services
{
    public class NotificationService : INotificationService
    {
        public ObservableCollection<NotificationMessage> Messages { get; } = [];

        public void Show(string title, string body, NotificationType type = NotificationType.Attention)
        {
            NotificationMessage note = new(title, body, type);

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Insert(0, note);
                if (Messages.Count > 3)
                {
                    Messages.RemoveAt(Messages.Count - 1);
                }
            });

            _ = Task.Run(async () =>
            {
                await Task.Delay(3000);
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    _ = Messages.Remove(note);
                });
            });
        }
    }
}