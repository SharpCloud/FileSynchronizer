using System;

namespace FileSynchronizer.Interfaces
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }
}
