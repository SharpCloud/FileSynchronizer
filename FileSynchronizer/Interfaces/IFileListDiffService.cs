using FileSynchronizer.Models;
using System.Collections.Generic;

namespace FileSynchronizer.Services
{
    public interface IFileListDiffService
    {
        DiffResult Compare(IList<string> filesInDirectory, IList<FileMetadata> lastRun);
        FileMetadataCollection Apply(DiffResult delta, IList<FileMetadata> target);
    }
}
