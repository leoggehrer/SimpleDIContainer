namespace SimpleDIContainer.ConApp
{
    internal class ConsoleMessageService : IMessageService
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Console: {message}");
        }
    }
}
