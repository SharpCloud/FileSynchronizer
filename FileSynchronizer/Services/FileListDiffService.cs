using FileSynchronizer.Interfaces;
using FileSynchronizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileSynchronizer.Services
{
    public class FileListDiffService
    {
        private readonly IFileHashService _fileHashService;

        public FileListDiffService(IFileHashService fileHashService)
        {
            _fileHashService = fileHashService;
        }

        public DiffResult Compare(IList<string> filesInDirectory, IList<FileMetadata> lastRun)
        {
            var diff = new DiffResult();

            var lastRunFilenames = lastRun.Select(m => m.Name);
            diff.Removed = lastRunFilenames.Except(filesInDirectory).ToList();

            foreach (var file in filesInDirectory)
            {
                var existing = lastRun.SingleOrDefault(m => m.Name == file);
                if (existing == null)
                {
                    diff.Added.Add(file);
                }
                else
                {
                    var hash = _fileHashService.ComputeHash(file);
                    if (hash != existing.Hash)
                    {
                        diff.Modified.Add(file);
                    }
                }
            }

            return diff;
        }
    }
}
