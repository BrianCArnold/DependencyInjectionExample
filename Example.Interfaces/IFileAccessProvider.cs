using System;

namespace Example.Interfaces
{
    [InjectableInterface]
    public interface IFileAccessProvider 
    {
        void Announce();
    }
}