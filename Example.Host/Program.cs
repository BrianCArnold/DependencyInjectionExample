using System;
using System.Linq;
using System.Reflection;
using Example.Injection;
using Example.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = new HostBuilder().ConfigureHostConfiguration(config => 
            {
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            }).ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true);
            }).ConfigureServices((builder, services) => 
            {
                var injectableInterfaces = InjectionSystem.CurrentlyLoadedAssemblies()
                    .GetTypesWithAttribute<InjectableInterfaceAttribute>();
                var injectableImplementations = InjectionSystem.CurrentlyLoadedAssemblies()
                    .GetTypesWithAttribute<InjectableImplementationAttribute>();
                foreach (var injectableInterface in injectableInterfaces)
                {
                    var matchingTypes = injectableImplementations.Where(t => injectableInterface.IsAssignableFrom(t));
                    foreach (var matchingType in matchingTypes)
                    {
                        if (typeof(ICustomService).IsAssignableFrom(matchingType))
                        {
                            typeof(ServiceCollectionHostedServiceExtensions)
                                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                                .Where(mi => mi.GetParameters().Length == 1)
                                .SingleOrDefault().MakeGenericMethod(matchingType).Invoke(null, new []{services});
                        } 
                        else 
                        {
                            var servDesc = new ServiceDescriptor(injectableInterface, matchingType, ServiceLifetime.Scoped);
                            services.Add(servDesc);
                        }
                    }
                }
            }).Build();
            var sqlAccess = host.Services.GetService<ISqlAccess>();
            if (sqlAccess != null)
            {
                Console.WriteLine("sql access implementation created");
            }
            else 
            {
                Console.WriteLine("sql access implementation not created");
            }
            //Recursively injects required constructor parameters and starts the 
            // host.Run();
        }
    }
}
