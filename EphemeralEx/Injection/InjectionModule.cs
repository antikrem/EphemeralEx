using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using EphemeralEx.Extensions;
using EphemeralEx.Reflection;


namespace EphemeralEx.Injection
{
    class MultipleInjectionPointsFoundException : Exception { };

    public static class InjectionModule
    {
        public static void AddRegisteredInjections(this IServiceCollection services)
        {
            GetInjectionDefinitions()
                .ForEach(
                        definition => services.Add(CreateServiceDescription(definition))
                    );
        }

        private static IEnumerable<(Type service, Type implementation)> GetInjectionDefinitions()
            => GetInjectableInterfaces()
                .Select(service => (service, GetImplementation(service)));

        private static ServiceDescriptor CreateServiceDescription((Type service, Type implementation) definition)
            => new ServiceDescriptor(definition.service, definition.implementation, ServiceLifetime.Singleton);

        private static Type GetImplementation(Type serviceEntry)
        {
            var implementations = ReflectionHelper
                .AllTypes
                .Where(implementation => serviceEntry.IsAssignableFrom(implementation) && implementation != serviceEntry);

            try
            {
                return implementations.Single();
            }
            catch (InvalidOperationException)
            {
                throw new MultipleInjectionPointsFoundException();
            }
        }

        private static IEnumerable<Type> GetInjectableInterfaces()
            => ReflectionHelper
                .AllTypes
                .Where(type => type.IsAttributed<Injectable>());
    }
}
