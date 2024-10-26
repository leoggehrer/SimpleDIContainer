namespace SimpleDIContainer.ConApp
{
    /// <summary>
    /// The entry point for the application, demonstrating a simple Dependency Injection (DI) container.
    /// </summary>
    /// <remarks>
    /// This class contains the <see cref="Main"/> method, which initializes the service container,
    /// registers services, and sends a notification using the registered <see cref="INotificationManager"/>.
    /// </remarks>
    internal class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// This method demonstrates a simple dependency injection container by registering services
        /// and resolving an instance of <see cref="INotificationManager"/> to send a notification.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the application.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Demo - Simple DI Container!");
            Console.WriteLine();

            var serviceContainer = new Logic.ServiceContainer();

            RegisterServices(serviceContainer);

            INotificationManager notificationManager = serviceContainer.Resolve<INotificationManager>();

            notificationManager.SendNotification("Notification to the beautiful World im Ersten!");
            Console.WriteLine();

            notificationManager = serviceContainer.Resolve<INotificationManager>();

            notificationManager.SendNotification("Notification to the beautiful World im Zweiten!");
        }

        /// <summary>
        /// Registers the required services in the specified service container.
        /// </summary>
        /// <param name="serviceContainer">The service container to which the services will be registered.</param>
        /// <remarks>
        /// This method registers the <see cref="IMessageService"/> with an implementation of <see cref="ConsoleMessageService"/>
        /// and the <see cref="INotificationManager"/> with an implementation of <see cref="NotificationManager"/>.
        /// </remarks>
        static void RegisterServices(Logic.ServiceContainer serviceContainer)
        {
            serviceContainer.Register<IMessageService, ConsoleMessageService>(true);
            serviceContainer.Register<INotificationManager, NotificationManager>(new Interceptor());
        }
    }
}
