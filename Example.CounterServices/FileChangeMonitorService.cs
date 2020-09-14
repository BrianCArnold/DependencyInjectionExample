using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Example.Interfaces;

namespace Example.CounterServices
{
    [InjectableImplementation]
    public class FileChangeMonitorService : ICustomService
    {
        private IDisposable Counter;
        public ICustomLogger Logger { get; }
        public IFileAccessProvider FileProvider { get; }
        public Dictionary<string, string> FileHashes = new Dictionary<string, string>();

        public FileChangeMonitorService(ICustomLogger logger, IFileAccessProvider fileProvider)
        {
            Logger = logger;
            FileProvider = fileProvider;
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
            var files = FileProvider.GetFileNames();
            foreach(var file in files)
            {
                if (!FileHashes.ContainsKey(file))
                {
                    FileHashes.Add(file, FileProvider.FileDigest(file));
                    yield return file;
                }
                else 
                {
                    var newHash = FileProvider.FileDigest(file);
                    var oldHash = FileHashes[file];
                    if (newHash != oldHash)
                    {
                        FileHashes[file] = newHash;
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
