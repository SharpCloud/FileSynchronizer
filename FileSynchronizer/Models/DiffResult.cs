using System.Collections.Generic;

namespace FileSynchronizer.Models
{
    public class DiffResult
    {
        public IList<string> Added { get; set; }
        public IList<string> Modified { get; set; }
        public IList<string> Removed { get; set; }

        public DiffResult()
        {
            Added = new List<string>();
            Modified = new List<string>();
            Removed = new List<string>();
        }
    }
}
