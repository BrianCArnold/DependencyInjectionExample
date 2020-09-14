using Example.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Example.AutoInjectionHost
{
    [InjectableImplementation]
    public class CustomConfiguration : ICustomConfiguration
    {

        public CustomConfiguration(IConfiguration config)
        {
            config.Bind(this);
        }

        public string DefaultFileAccessDirectory { get; set; }
    }
}