using System.Collections.Generic;
using Example.Models;
using Microsoft.Extensions.Hosting;

namespace Example.Interfaces
{
    [InjectableInterface]
    public interface ISqlAccess
    {
        IEnumerable<FileData> GetFiles();
        string GetFileHash(string fileName);
        void SetFileHash(string fileName, string hash);
    }
}