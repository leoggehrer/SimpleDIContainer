namespace SimpleDIContainer.Logic
{
    public class ServiceContainer
    {
        // Dictionary zum Speichern von Typzuweisungen (z.B. Interface -> Implementierung)
        private Dictionary<Type, Type> _typeMappings = new Dictionary<Type, Type>();

        // Registrierung: Ein Interface wird einer konkreten Implementierung zugewiesen
        public void Register<TInterface, TImplementation>()
        {
            _typeMappings[typeof(TInterface)] = typeof(TImplementation);
        }

        // Auflösung: Der Container erstellt eine Instanz der gewünschten Klasse
        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        private object Resolve(Type type)
        {
            // Wenn der Typ nicht registriert wurde, werfen wir eine Exception
            if (!_typeMappings.ContainsKey(type))
            {
                throw new Exception($"Type {type.Name} not registered in the container");
            }

            // Die konkrete Implementierung für das registrierte Interface
            Type implementationType = _typeMappings[type];

            // Den Standard-Konstruktor verwenden, um eine Instanz der Implementierung zu erzeugen
            var constructorInfo = implementationType.GetConstructors()[0];
            var parameters = constructorInfo.GetParameters();

            if (parameters.Length == 0)
            {
                // Wenn der Konstruktor keine Parameter hat, einfach die Instanz erstellen
                return Activator.CreateInstance(implementationType) ?? throw new InvalidOperationException($"Could not create an instance of {implementationType.FullName}");
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

                return constructorInfo.Invoke([.. parameterImplementations]);
            }
        }
    }
}
