using System.Collections.Generic;

namespace FileSynchronizer.Models
{
    public class DiffResult
    {
        public IList<FileMetadata> Added { get; set; } = new List<FileMetadata>();
        public IList<FileMetadata> Modified { get; set; } = new List<FileMetadata>();
        public IList<string> Removed { get; set; } = new List<string>();
    }
}
