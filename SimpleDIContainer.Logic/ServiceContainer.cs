using Castle.DynamicProxy;

namespace SimpleDIContainer.Logic
{
    /// <summary>
    /// Represents a service container that manages the registration and resolution of service types.
    /// </summary>
    /// <remarks>
    /// This class allows for the mapping of interfaces to their concrete implementations,
    /// enabling dependency injection and facilitating the creation of instances of registered types.
    /// </remarks>
    public class ServiceContainer
    {
        #region embedded types
        private class ImplementationInfo
        {
            /// <summary>
            /// Gets or sets the type of the implementation.
            /// </summary>
            public required Type Type { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the implementation is a singleton.
            /// </summary>
            public required bool IsSingleton { get; set; }

            /// <summary>
            /// Gets or sets the interceptor to be applied to the implementation.
            /// </summary>
            public IInterceptor? Interceptor { get; set; }

            /// <summary>
            /// Gets or sets the instance of the implementation.
            /// </summary>
            public object? Instance { get; set;}
        }
        #endregion embedded types

        #region fields
        private readonly ProxyGenerator _proxyGenerator = new();
        // Dictionary for storing type mappings (e.g. Interface -> Implementation)
        private readonly Dictionary<Type, ImplementationInfo> _typeMappings = [];
        #endregion fields

        #region register methods
        /// <summary>
        /// Registers a mapping between a specified interface and its implementation.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface to be registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that will be associated with the interface.</typeparam>
        /// <remarks>
        /// This method adds a mapping to the internal collection, allowing for later resolution of the interface to its implementation.
        /// </remarks>
        public void Register<TInterface, TImplementation>()
        {
            Register<TInterface, TImplementation>(false, null);
        }

        /// <summary>
        /// Registers a mapping between a specified interface and its implementation.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface to be registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that will be associated with the interface.</typeparam>
        /// <param name="isSingleton">Indicates if the implementation type is a singleton.</param>
        public void Register<TInterface, TImplementation>(bool isSingleton)
        {
            Register<TInterface, TImplementation>(isSingleton, null);
        }

        /// <summary>
        /// Registers a mapping between a specified interface and its implementation with an interceptor.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface to be registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that will be associated with the interface.</typeparam>
        /// <param name="interceptor">The interceptor to apply to the implementation.</param>
        public void Register<TInterface, TImplementation>(IInterceptor? interceptor)
        {
            Register<TInterface, TImplementation>(false, interceptor);
        }

        /// <summary>
        /// Registers a mapping between a specified interface and its implementation with an interceptor and singleton option.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface to be registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that will be associated with the interface.</typeparam>
        /// <param name="isSingleton">Indicates if the implementation type is a singleton.</param>
        /// <param name="interceptor">The interceptor to apply to the implementation.</param>
        public void Register<TInterface, TImplementation>(bool isSingleton, IInterceptor? interceptor)
        {
            Type interfaceType = typeof(TInterface);
            Type implementationType = typeof(TImplementation);

            _typeMappings[interfaceType] = new ImplementationInfo
            {
                Type = implementationType,
                IsSingleton = isSingleton,
                Interceptor = interceptor,
                Instance = null,
            };
        }
        #endregion register methods

        #region resolve methods
        /// <summary>
        /// Resolves an instance of the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface to resolve.</typeparam>
        /// <returns>An instance of the specified interface type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the requested interface cannot be resolved.</exception>
        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        /// <summary>
        /// Resolves an instance of the specified type by retrieving its registered implementation
        /// and invoking its constructor, resolving any dependencies recursively.
        /// </summary>
        /// <param name="type">The type to resolve an instance for.</param>
        /// <returns>An instance of the specified type.</returns>
        /// <exception cref="Exception">Thrown when the specified type is not registered in the container.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an instance of the implementation type cannot be created.</exception>
        private object Resolve(Type type)
        {
            object result;

            // Wenn der Typ nicht registriert wurde, werfen wir eine Exception
            if (!_typeMappings.TryGetValue(type, out ImplementationInfo? value))
            {
                throw new Exception($"Type {type.Name} not registered in the container");
            }

            // Die konkrete Implementierung für das registrierte Interface
            ImplementationInfo implementationInfo = value;
            Type implementationType = implementationInfo.Type;

            if (implementationInfo.IsSingleton == false 
                || implementationInfo.Instance == null)
            {
                // Den Standard-Konstruktor verwenden, um eine Instanz der Implementierung zu erzeugen
                var constructorInfo = implementationType.GetConstructors()[0];
                var parameters = constructorInfo.GetParameters();

                if (parameters.Length == 0)
                {
                    // Wenn der Konstruktor keine Parameter hat, einfach die Instanz erstellen
                    result = Activator.CreateInstance(implementationType) ?? throw new InvalidOperationException($"Could not create an instance of {implementationType.FullName}");
                }
                else
                {
                    // Wenn der Konstruktor Parameter hat, rekursiv deren Abhängigkeiten auflösen
                    var parameterImplementations = new List<object>();

                    foreach (var parameter in parameters)
                    {
                        var parameterInstance = Resolve(parameter.ParameterType);

                        parameterImplementations.Add(parameterInstance);
                    }
                    result = constructorInfo.Invoke([.. parameterImplementations]);
                }
                if (implementationInfo.IsSingleton)
                {
                    implementationInfo.Instance = result;
                }
            }
            else    // implementationInfo.IsSingleton 
                    // && implementationInfo.Instance != null
            {
                result = implementationInfo.Instance;
            }

            return implementationInfo.Interceptor != default ? _proxyGenerator.CreateInterfaceProxyWithTarget(type, result, implementationInfo.Interceptor) : result;
        }
        #endregion resolve methods
    }
}
