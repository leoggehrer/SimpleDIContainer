namespace SimpleDIContainer.ConApp
{
    internal class EmailMessageService : IMessageService
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Email message sent: {message}"); 
        }
    }
}
