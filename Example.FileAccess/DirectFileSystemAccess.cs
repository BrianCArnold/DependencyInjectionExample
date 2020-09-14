using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Example.Interfaces;

namespace Example.FileAccess
{
    [InjectableImplementation]
    public class DirectFileSystemAccess : IFileAccessProvider
    {
        public DirectFileSystemAccess(ICustomConfiguration config, HashAlgorithm hashingSystem)
        {
            Config = config;
            HashingSystem = hashingSystem;
        }

        public ICustomConfiguration Config { get; }
        public HashAlgorithm HashingSystem { get; }

        public string FileContents(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public string FileDigest(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                var hashData = HashingSystem.ComputeHash(fileStream);
                return Convert.ToBase64String(hashData);
            }
        }

        public IEnumerable<string> GetFileNames()
        {
            var dir = new FileInfo( Assembly.GetExecutingAssembly().Location).Directory;
            return dir.GetFiles().Select(f => f.FullName);
        }
    }
}
