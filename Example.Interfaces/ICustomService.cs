using Microsoft.Extensions.Hosting;

namespace Example.Interfaces
{
    [InjectableInterface]
    public interface ICustomService : IHostedService
    {

    }
}