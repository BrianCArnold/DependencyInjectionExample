using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.Interfaces;

namespace Example.CounterServices
{
    [InjectableImplementation]
    public class FizzBuzzService : ICustomService
    {
        private IDisposable Counter;
        public ICustomLogger Logger { get; }

        public FizzBuzzService(ICustomLogger logger)
        {
            Logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Counter = Observable.Interval(TimeSpan.FromSeconds(1)).Skip(1).Subscribe(num => {
                var fizz = num % 3 == 0 ? "Fizz" : "";
                var buzz = num % 5 == 0 ? "Buzz" : "";
                Console.WriteLine(((fizz + buzz).Length > 0) ? fizz + buzz : num.ToString());
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Counter.Dispose();
            return Task.CompletedTask;
        }
    }
}
