using FileSynchronizer.Interfaces;
using FileSynchronizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileSynchronizer.Services
{
    public class FileListDiffService : IFileListDiffService
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
                var hash = _fileHashService.ComputeHash(file);
                var existing = lastRun.SingleOrDefault(m => m.Name == file);

                var metadata = new FileMetadata
                {
                    Hash = hash,
                    Name = file
                };

                if (existing == null)
                {
                    diff.Added.Add(metadata);
                }
                else if (hash != existing.Hash)
                {
                    diff.Modified.Add(metadata);
                }
            }

            return diff;
        }

        public FileMetadataCollection Apply(DiffResult delta, IList<FileMetadata> target)
        {
            return new FileMetadataCollection
            {
                Files = (List<FileMetadata>)target
            };
        }
    }
}
