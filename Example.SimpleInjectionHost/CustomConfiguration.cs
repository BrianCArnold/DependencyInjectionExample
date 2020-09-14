using Example.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Example.SimpleInjectionHost
{
    [InjectableImplementation]
    public class CustomConfiguration : ICustomConfiguration
    {
        public CustomConfiguration(IConfiguration config)
        {
            config.Bind(this);
        }

        public string CustomConfigOption { get; set; }
    }
}