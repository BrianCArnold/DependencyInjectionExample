using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
                    //Adds the Custom Logger as an implementation of ICustomLogger.
                    .AddScoped<ICustomLogger, ConsoleLogger>()
                    //Adds a Mock of what an EntityFramework context might look like.
                    .AddScoped<ISqlAccess, SqlMockAccess>()
                    //Adds a Custom Configuration Mapping
                    .AddScoped<ICustomConfiguration, CustomConfiguration>()
                    //Adds a File Access Provider
                    .AddScoped<IFileAccessProvider, DirectFileSystemAccess>()
                    //Adds a Digest Provider
                    .AddScoped<HashAlgorithm, SHA512Managed>()
                    //Adds the Fizz Buzz Service as something to be run by the Host service.
                    .AddHostedService<FileChangeMonitorService>()
                    .AddHostedService<FizzBuzzService>();
            })
            .Build();
            host.Run();
        }
    }
}
