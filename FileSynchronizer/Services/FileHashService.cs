using FileSynchronizer.Interfaces;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileSynchronizer.Services
{
    public class FileHashService : IFileHashService
    {
        public string ComputeHash(string filepath)
        {
            byte[] hashedBytes;

            using (var sha1 = new SHA1Cng())
            {
                var fileStream = File.ReadAllBytes(filepath);
                hashedBytes = sha1.ComputeHash(fileStream);
            }

            var hashCode = ConvertBytesToHexString(hashedBytes);
            return hashCode;
        }

        private string ConvertBytesToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var byteValue in bytes)
            {
                var stringValue = $"{byteValue:X2}";
                sb.Append(stringValue);
            }

            var finalString = sb.ToString();
            return finalString;
        }
    }
}
