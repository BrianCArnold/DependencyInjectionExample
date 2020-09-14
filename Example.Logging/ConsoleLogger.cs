using System;
using Example.Interfaces;

namespace Example.Logging
{
    [InjectableImplementation]
    public class ConsoleLogger : ICustomLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
