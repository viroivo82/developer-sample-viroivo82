using System;
using System.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        // dictionary of interface types to implementation types
        private readonly Dictionary<Type, Type> _bindings = new Dictionary<Type, Type>();
        public void Bind(Type interfaceType, Type implementationType)
        {
            _bindings[interfaceType] = implementationType;
        }
        public T Get<T>()
        {
            var type = typeof(T);
            if (_bindings.TryGetValue(type, out var implementationType))
            {
                return (T)Activator.CreateInstance(implementationType);
            }

            // if we don't have a binding for the type, throw an exception
            throw new InvalidOperationException($"No binding found for type {type.FullName}");
        }
    }
}