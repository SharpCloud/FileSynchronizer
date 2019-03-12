using FileSynchronizer.Interfaces;
using FileSynchronizer.Models;
using FileSynchronizer.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FileSynchronizer.Test
{
    [TestFixture]
    public class FileListDiffServiceTests
    {
        [Test]
        public void NewFilesAreIdentified()
        {
            // Arrange

            const string file1 = "File1";
            const string file2 = "File2";
            const string file2Hash = "File2Hash";

            var filesInDirectory = new[] { file1, file2 };

            var lastRun = new FileMetadataCollection
            {
                Files = new List<FileMetadata>
                {
                    new FileMetadata { Name = file1 }
                }
            };

            var hashService = Mock.Of<IFileHashService>(s =>
                s.ComputeHash(file2) == file2Hash);

            var diff = new FileListDiffService(hashService);

            // Act

            var result = diff.Compare(filesInDirectory, lastRun.Files);

            // Assert

            Assert.AreEqual(1, result.Added.Count);
            Assert.AreEqual(file2Hash, result.Added[0].Hash);
            Assert.AreEqual(file2, result.Added[0].Name);
            Assert.IsEmpty(result.Modified);
            Assert.IsEmpty(result.Removed);
        }

        [Test]
        public void ModifiedFilesAreIdentified()
        {
            // Arrange

            const string file1 = "File1";
            const string file1Hash = "File1Hash";
            const string file2 = "File2";
            const string file2ModifiedHash= "File2ModifiedHash";

            var filesInDirectory = new[] { file1, file2 };

            var hashService = Mock.Of<IFileHashService>(s =>
                s.ComputeHash(file1) == file1Hash &&
                s.ComputeHash(file2) == file2ModifiedHash);

            var lastRun = new FileMetadataCollection
            {
                Files = new List<FileMetadata>
                {
                    new FileMetadata
                    {
                        Hash = file1Hash,
                        Name = file1
                    },
                    new FileMetadata
                    {
                        Hash = "File2Hash",
                        Name = file2
                    }
                }
            };

            var diff = new FileListDiffService(hashService);

            // Act

            var result = diff.Compare(filesInDirectory, lastRun.Files);

            // Assert

            Assert.IsEmpty(result.Added);
            Assert.AreEqual(1, result.Modified.Count);
            Assert.AreEqual(file2ModifiedHash, result.Modified[0].Hash);
            Assert.AreEqual(file2, result.Modified[0].Name);
            Assert.IsEmpty(result.Removed);
        }

        [Test]
        public void RemovedFilesAreIdentified()
        {
            // Arrange

            const string file1 = "File1";
            const string file2 = "File2";

            var filesInDirectory = new[] { file1 };

            var lastRun = new FileMetadataCollection
            {
                Files = new List<FileMetadata>
                {
                    new FileMetadata { Name = file1 },
                    new FileMetadata { Name = file2 }
                }
            };

            var hashService = Mock.Of<IFileHashService>();
            var diff = new FileListDiffService(hashService);

            // Act

            var result = diff.Compare(filesInDirectory, lastRun.Files);

            // Assert

            Assert.IsEmpty(result.Added);
            Assert.IsEmpty(result.Modified);
            Assert.AreEqual(1, result.Removed.Count);
            Assert.AreEqual(file2, result.Removed[0]);
        }
    }
}
