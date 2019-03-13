using FileSynchronizer.Interfaces;
using System;

namespace FileSynchronizer.Services
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
