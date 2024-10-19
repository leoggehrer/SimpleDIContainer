namespace SimpleDIContainer.ConApp
{
    /// <summary>
    /// Provides a service for sending messages to the console.
    /// </summary>
    /// <remarks>
    /// This class implements the <see cref="IMessageService"/> interface
    /// and outputs messages to the console window.
    /// </remarks>
    internal class ConsoleMessageService : IMessageService
    {
        /// <summary>
        /// Sends a message to the console output.
        /// </summary>
        /// <param name="message">The message to be sent to the console.</param>
        public void SendMessage(string message)
        {
            Console.WriteLine($"Console: {message}");
        }
    }
}
