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
            GetInjectableInterfaces()
                .ForEach(service => services.Add(CreateRegistration(service, GetImplementation(service))));

            GetMultipleInjectableInterfaces()
                .ForEach(
                    service => GetImplementations(service)
                        .ForEach(implementation=> services.Add(CreateRegistration(service, implementation)))
                );
        }


        private static ServiceDescriptor CreateRegistration(Type service, Type implementation)
            => new(service, implementation, ServiceLifetime.Singleton);

        private static IEnumerable<Type> GetInjectableInterfaces()
            => ReflectionHelper
                .AllTypes
                .Where(type => type.IsAttributed<Injectable>());

        private static IEnumerable<Type> GetMultipleInjectableInterfaces()
            => ReflectionHelper
                .AllTypes
                .Where(type => type.IsAttributed<MultipleInjectable>());

        private static Type GetImplementation(Type serviceEntry)
        {
            try
            {
                return GetImplementations(serviceEntry).Single();
            }
            catch (InvalidOperationException)
            {
                throw new MultipleInjectionPointsFoundException();
            }
        }

        private static IEnumerable<Type> GetImplementations(Type serviceEntry)
            => ReflectionHelper
                .AllTypes
                .Where(implementation => serviceEntry.IsAssignableFrom(implementation) && implementation != serviceEntry);
    }
}
