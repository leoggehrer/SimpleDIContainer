namespace SimpleDIContainer.ConApp
{
    public class NotificationManager : INotificationManager
    {
        private readonly IMessageService _messageService;

        // Constructor Injection
        public NotificationManager(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void SendNotification(string message)
        {
            _messageService.SendMessage(message);
        }
    }
}
