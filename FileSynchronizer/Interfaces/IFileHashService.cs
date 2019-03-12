namespace FileSynchronizer.Interfaces
{
    public interface IFileHashService
    {
        string ComputeHash(string filepath);
    }
}
