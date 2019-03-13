using System;
using System.Collections.Generic;

namespace FileSynchronizer.Models
{
    public class FileMetadataCollection
    {
        public DateTime LastUpdated { get; set; }
        public List<FileMetadata> Files { get; set; } = new List<FileMetadata>();
    }
}
