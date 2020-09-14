using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Example.Interfaces;

namespace Example.Injection
{
    public static class InjectionSystem
    {
        public static IEnumerable<Assembly> CurrentlyLoadedAssemblies() => 
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.Location?.Contains(".dll") ?? false).ToArray();
        static InjectionSystem()
        {
            var currentAssemblies = CurrentlyLoadedAssemblies();
            foreach (var posA in new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.GetFiles("*.dll"))
            {
                if (!currentAssemblies.Any(a => a.Location == posA.FullName))
                {
                    AppDomain.CurrentDomain.Load(Assembly.LoadFile(posA.FullName).FullName);
                }
            }
        }
        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(this IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute =>
                assemblies.SelectMany(a => 
                    a.GetExportedTypes()
                        .Where(t => t.GetCustomAttribute<TAttribute>() != null)
                );
        public static IEnumerable<Type> GetInjectableInterfaces() =>
            CurrentlyLoadedAssemblies()
            .GetTypesWithAttribute<InjectableInterfaceAttribute>();
        public static IEnumerable<Type> GetInjectionProviderTypes() => 
            CurrentlyLoadedAssemblies()
            .GetTypesWithAttribute<InjectableImplementationAttribute>();
        
        
    }
}
