using System.IO;

namespace SanityArchiver.Application.Models.FileHandling
{
    /// <summary>
    /// This class handles 5 operations on FileInfo objects. Move, Copy, Rename, Change extension, Change visibility.
    /// </summary>
    public static class FileHandler
    {
        /// <summary>
        /// Moves FileInfo object to the destination directory (entered as a string). Uses the File.Copy method then deletes the original file.
        /// </summary>
        /// <param name="file">FileInfo</param>
        /// <param name="destination">string</param>
        public static void Move(FileInfo file, DirectoryInfo destination)
        {
            string destFile = CreateDestPath(file, destination.FullName);
            file.MoveTo(destFile);
        }

        /// <summary>
        /// Renames or changes extension of a file
        /// </summary>
        /// <param name="file">file to modify</param>
        /// <param name="destination">destination folder</param>
        /// <param name="fileName">new filename</param>
        /// <param name="extension">new extension</param>
        public static void Move(FileInfo file, DirectoryInfo destination, string fileName, string extension)
        {
            file.MoveTo(Path.Combine(destination.FullName, fileName + "." + extension));
        }

        /// <summary>
        /// Copies a FileInfo object to the destination folder.Uses the File.Copy method.
        /// </summary>
        /// <param name="file">FileInfo</param>
        /// <param name="destination">string</param>
        public static void Copy(FileInfo file, DirectoryInfo destination)
        {
            file.CopyTo(Path.Combine(destination.FullName, file.Name));
        }

        /// <summary>
        /// Renames the subject file passed as a FileInfo object. The new name does NOT need the extension added. The method keeps the original extension.
        /// </summary>
        /// <param name="file">FileInfo</param>
        /// <param name="newName">string</param>
        public static void ChangeName(FileInfo file, string newName)
        {
            file.MoveTo(file.Directory.FullName + "\\" + newName + Path.GetExtension(file.FullName));
        }

        /// <summary>
        /// Changes the extension of a file passed as a FileInfo object. New extension is passed as a string. The extension needs the "." in front of it, for example ".txt", ".ppt" etc.
        /// </summary>
        /// <param name="file">FileInfo</param>
        /// <param name="extension">string</param>
        public static void ChangeExtension(FileInfo file, string extension)
        {
            File.Move(file.FullName, Path.ChangeExtension(file.FullName, extension));
        }

        /// <summary>
        /// Changes the visibility of a file passed as a FileInfo object. Use boolean false to hide, true to reveal the file.
        /// </summary>
        /// <param name="file">FileInfo</param>
        /// <param name="visible">boolean</param>
        public static void ModifyVisibility(FileInfo file, string visible)
        {
            if (visible.Equals("Visible") == true)
            {
                file.Attributes = FileAttributes.Normal;
            }
            else if (visible.Equals("Hidden") == true)
            {
                file.Attributes = FileAttributes.Hidden;
            }
        }

        /// <summary>
        /// Deletes the given parameter.
        /// </summary>
        /// <param name="file">FileInfo object</param>
        public static void DeleteFile(FileInfo file)
        {
            file.Delete();
        }

        private static string CreateDestPath(FileInfo file, string destination)
        {
            return Path.Combine(destination, file.Name);
        }
    }
}
