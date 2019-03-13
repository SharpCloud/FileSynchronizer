using FileSynchronizer.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace FileSynchronizer.Services
{
    public class DirectoryDiffService
    {
        private const string LastRunDataFileName = "file_synchronizer_last_run.txt";

        private readonly IFileListDiffService _fileListDiffService;

        public DirectoryDiffService(IFileListDiffService fileListDiffService)
        {
            _fileListDiffService = fileListDiffService;
        }

        public void DetectModifications(string directoryPath)
        {
            FileMetadataCollection lastRun;

            var lastRunFilePath = Path.Combine(directoryPath, LastRunDataFileName);
            var exists = File.Exists(LastRunDataFileName);

            if (exists)
            {
                var fileText = File.ReadAllText(lastRunFilePath);
                lastRun = JsonConvert.DeserializeObject<FileMetadataCollection>(fileText);
            }
            else
            {
                lastRun = new FileMetadataCollection();
            }

            var filelist = Directory.EnumerateFiles(directoryPath)
                .Where(f => !f.EndsWith(LastRunDataFileName))
                .ToList();

            var delta = _fileListDiffService.Compare(filelist, lastRun.Files);
            var updated = _fileListDiffService.Apply(delta, lastRun.Files);
            var updatedJson = JsonConvert.SerializeObject(updated);
            File.WriteAllText(lastRunFilePath, updatedJson);
        }
    }
}
