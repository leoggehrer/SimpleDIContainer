namespace SimpleDIContainer.ConApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo - Simple DI Container!");

            var serviceContainer = new Logic.ServiceContainer();

            RegisterServices(serviceContainer);

            INotificationManager notificationManager = serviceContainer.Resolve<INotificationManager>();

            notificationManager.SendNotification("Hello, World!");
        }

        static void RegisterServices(Logic.ServiceContainer serviceContainer)
        {
            serviceContainer.Register<IMessageService, EmailMessageService>();
            serviceContainer.Register<INotificationManager, NotificationManager>();
        }
    }
}
