using System;
using System.Collections.Generic;

namespace Example.Interfaces
{
    [InjectableInterface]
    public interface IFileAccessProvider 
    {
        IEnumerable<string> GetFileNames();
        string FileContents(string fileName);
        string FileDigest(string fileName);
    }
}