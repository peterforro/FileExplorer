using System.IO;
using System.IO.Compression;

namespace SanityArchiver.Application.Models.Archiver
{
    /// <summary>
    /// This class has methods, which has the responsibility to compress and decompress files.
    /// 2 methods: Compress and Decompress. All of these methods get 2 parameters: a FileInfo object and a string path.
    /// </summary>
    public class FileArchiver
    {
        /// <summary>
        /// This method can be used for compressing a file.
        /// This method uses 2 different streams: a FileStream and a GZipStream also.
        /// FileStream initializes a new instance of the FileStream class with the specified path, creation mode,
        /// read/write permission and sharing permission.
        /// GZipStream initializes a new instance of the GZipStream class by using the specified stream and compression mode.
        /// </summary>
        /// <param name="file">This parameter is a FileInfo object.</param>
        /// <param name="destination">This parameter is a string, which is the destination path.</param>
        public void Compress(FileInfo file, DirectoryInfo destination)
        {
            using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var compressedFile = File.Create(Path.Combine(destination.FullName, $"{file.Name}.gz")))
                {
                    using (var compressionStream = new GZipStream(compressedFile, CompressionMode.Compress))
                    {
                        fileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        /// This methods can be used for decompressing a file.
        /// This method uses 2 different streams: a FileStream and also a GZipStream.
        /// FileStream initializes a new instance of the FileStream class with the specified path, creation mode,
        /// read/write permission and sharing permission.
        /// GZipStream initializes a new instance of the GZipStream class by using the specified stream and compression mode.
        /// </summary>
        /// <param name="file">This parameter is a FileInfo object.</param>
        /// <param name="destinationPath">This parameter is a string, which is the destination path.</param>
        public void Decompress(FileInfo file, string destinationPath)
        {
            using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var decompressedFile = File.Create(destinationPath))
                {
                    using (var decompressionStream = new GZipStream(decompressedFile, CompressionMode.Decompress))
                    {
                        fileStream.CopyTo(decompressionStream);
                    }
                }
            }
        }
    }
}
