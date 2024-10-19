namespace SimpleDIContainer.ConApp
{
    /// <summary>
    /// Defines a contract for sending notifications.
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// Sends a notification with the specified message.
        /// </summary>
        /// <param name="message">The message to be sent in the notification.</param>
        /// <remarks>
        /// This method is responsible for delivering notifications to the intended recipients.
        /// Ensure that the message is formatted correctly before calling this method.
        /// </remarks>
        void SendNotification(string message);
    }
}