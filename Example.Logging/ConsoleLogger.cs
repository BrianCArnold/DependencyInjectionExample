using System;
using Example.Interfaces;

namespace Example.Logging
{
    public class ConsoleLogger : ICustomLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
