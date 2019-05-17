using System;
using System.Collections.Generic;
using System.IO;

namespace SanityArchiver.Application.Models.Search
{
    /// <summary>
    /// Search among the files whit a given directory path and pattern.
    /// The search is recursive in the selected folder.
    /// </summary>
    public class FileSeeker
    {
        /// <inheritdoc/>
        public List<FileInfo> Search(DirectoryInfo rootDir, string fileName)
        {
            var foundedFiles = SearchFile(rootDir, fileName);
            return foundedFiles;
        }

        /// <inheritdoc/>
        private List<FileInfo> SearchFile(DirectoryInfo rootDir, string pattern)
        {
            var foundedFiles = new List<FileInfo>();
            RecursiveTraversing(rootDir, pattern, foundedFiles);
            return foundedFiles;
        }

        /// <inheritdoc/>
        private void RecursiveTraversing(DirectoryInfo directoryInfo, string pattern, List<FileInfo> foundedFiles)
        {
            FileInfo[] files = null;
            try
            {
                files = directoryInfo.GetFiles(pattern);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (NullReferenceException)
            {
            }

            if (files != null)
            {
                foreach (var fileInfo in files)
                {
                    foundedFiles.Add(fileInfo);
                }

                foreach (var subDir in directoryInfo.GetDirectories())
                {
                    RecursiveTraversing(subDir, pattern, foundedFiles);
                }
            }
        }
    }
}