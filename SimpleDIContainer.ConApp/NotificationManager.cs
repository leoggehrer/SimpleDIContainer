namespace SimpleDIContainer.ConApp
{
    /// <summary>
    /// Manages the sending of notifications by utilizing an <see cref="IMessageService"/>.
    /// </summary>
    /// <remarks>
    /// This class is responsible for sending notifications through the specified message service.
    /// It uses constructor injection to receive an instance of <see cref="IMessageService"/>.
    /// </remarks>
    public class NotificationManager : INotificationManager
    {
        private readonly IMessageService _messageService;

        // Constructor Injection
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationManager"/> class.
        /// </summary>
        /// <param name="messageService">The message service used to send notifications.</param>
        public NotificationManager(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Sends a notification message using the message service.
        /// </summary>
        /// <param name="message">The message to be sent as a notification.</param>
        public void SendNotification(string message)
        {
            _messageService.SendMessage(message);
        }
    }
}
