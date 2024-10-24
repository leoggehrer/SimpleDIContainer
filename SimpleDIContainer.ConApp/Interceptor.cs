using Castle.DynamicProxy;

namespace SimpleDIContainer.ConApp
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Vor Aufruf: {invocation.Method.Name}");

            invocation.Proceed();  // Methode wird aufgerufen

            Console.WriteLine($"Nach Aufruf: {invocation.Method.Name}");
        }
    }
}
