using System;
using System.Collections.Generic;
using System.Linq;
using Example.Interfaces;
using Example.Models;

namespace Example.SqlAccess
{
    [InjectableImplementation]
    public class SqlMockAccess : ISqlAccess
    {
        private List<FileData> Files { get; set; } = new List<FileData>();

        public string GetFileHash(string fileName)
        {
            if (!Files.Any(f => f.Name == fileName))
            {
                return string.Empty;
            }
            else 
            {
                return Files.SingleOrDefault(f => f.Name == fileName).Hash;
            }
        }

        public IEnumerable<FileData> GetFiles()
        {
            return Files.ToArray();
        }

        public void SetFileHash(string fileName, string hash)
        {
            
            if (!Files.Any(f => f.Name == fileName))
            {
                Files.Add(new FileData{
                    Hash = hash,
                    Name = fileName,
                    Id = Files.Aggregate(1, (a,n) => a > n.Id ? a : n.Id, a => a + 1)
                });
            }
            else 
            {
                Files.SingleOrDefault(f => f.Hash == hash).Hash = hash;
            }
        }
    }
}
