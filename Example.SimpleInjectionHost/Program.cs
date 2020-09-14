using System;
using System.Linq;
using System.Reflection;
using Example.CounterServices;
using Example.FileAccess;
using Example.Interfaces;
using Example.Logging;
using Example.SqlAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example.SimpleInjectionHost
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
                services
                    .AddScoped<ICustomLogger, ConsoleLogger>()
                    .AddScoped<ISqlAccess, EntityFrameworkMockAccess>()
                    .AddScoped<ICustomConfiguration, CustomConfiguration>()
                    .AddScoped<IFileAccessProvider, DirectFileSystemAccess>()
                    .AddHostedService<FizzBuzzService>();
            })
            .Build();
            host.Run();
        }
    }
}
