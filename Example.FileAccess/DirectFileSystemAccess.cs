using System;
using Example.Interfaces;

namespace Example.FileAccess
{
    [InjectableImplementation]
    public class DirectFileSystemAccess : IFileAccessProvider
    {
        public void Announce()
        {
            Console.WriteLine($"{nameof(DirectFileSystemAccess)} Loaded");
        }
    }
}
