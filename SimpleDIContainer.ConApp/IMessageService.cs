
namespace SimpleDIContainer.ConApp
{
    /// <summary>
    /// Defines a service for sending messages.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Sends a message to the designated recipient.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <exception cref="ArgumentNullException">Thrown when the message is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the message cannot be sent due to the current state.</exception>
        void SendMessage(string message);
    }
}
