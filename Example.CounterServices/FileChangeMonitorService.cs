using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Example.Interfaces;
using Example.Models;

namespace Example.CounterServices
{
    [InjectableImplementation]
    public class FileChangeMonitorService : ICustomService
    {
        private IDisposable Counter;
        public ICustomLogger Logger { get; }
        public IFileAccessProvider FileProvider { get; }
        public ISqlAccess Sql { get; }

        public FileChangeMonitorService(ICustomLogger logger, IFileAccessProvider fileProvider, ISqlAccess sql)
        {
            Logger = logger;
            FileProvider = fileProvider;
            Sql = sql;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Counter = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(num => {
                var changedFiles = CheckForChangedFiles();
                foreach (var file in changedFiles)
                {
                    Console.WriteLine($"File changed: {file}");
                }
            });
            return Task.CompletedTask;
        }

        private IEnumerable<string> CheckForChangedFiles()
        {
            var files = FileProvider.GetFileNames().ToArray();
            var existingFileInfo = Sql.GetFiles();
            foreach(var file in files)
            {
                if (!existingFileInfo.Any(fi => fi.Name == file))
                {
                    Sql.SetFileHash(file, FileProvider.FileDigest(file));
                    yield return file;
                }
                else 
                {
                    var newHash = FileProvider.FileDigest(file);
                    var oldHash = Sql.GetFileHash(file);
                    if (newHash != oldHash)
                    {
                        Sql.SetFileHash(file, newHash);
                        yield return file;
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Counter.Dispose();
            return Task.CompletedTask;
        }
    }
}
