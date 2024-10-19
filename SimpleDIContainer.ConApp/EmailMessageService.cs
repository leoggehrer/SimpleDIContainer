namespace SimpleDIContainer.ConApp
{
    /// <summary>
    /// Provides functionality to send email messages.
    /// </summary>
    /// <remarks>
    /// This class implements the <see cref="IMessageService"/> interface to send messages via email.
    /// </remarks>
    internal class EmailMessageService : IMessageService
    {
        /// <summary>
        /// Sends an email message with the specified content.
        /// </summary>
        /// <param name="message">The content of the email message to be sent.</param>
        /// <remarks>This method simulates sending an email by writing the message to the console.</remarks>
        public void SendMessage(string message)
        {
            Console.WriteLine($"Email message sent: {message}");
        }
    }
}
