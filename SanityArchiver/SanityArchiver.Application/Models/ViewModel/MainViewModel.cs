using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using SanityArchiver.Application.Models.Archiver;
using SanityArchiver.Application.Models.Node;
using SanityArchiver.Application.Models.Search;
using SanityArchiver.Application.Models.FileHandling;

namespace SanityArchiver.Application.Models.ViewModel
{
    /// <summary>
    /// The main aggregator class that connects the model classes with the UI
    /// </summary>
    public class MainViewModel
    {
        private readonly FileSeeker _fileSeeker = new FileSeeker();
        private readonly FileArchiver _fileArchiver = new FileArchiver();
        private HandlerDelegate _handlerDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// It loads the nodes that are representing the root directory of the logical drives
        /// </summary>
        public MainViewModel()
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var root = new DirectoryInfo(drive);
                var rootNode = new FileSystemNode(root);
                rootNode.LoadContent();
                Nodes.Add(rootNode);
            }
        }

        private delegate void HandlerDelegate(FileInfo file, DirectoryInfo dir);

        /// <summary>
        /// Gets or sets the files contained by the actual node (directory)
        /// </summary>
        public ObservableCollection<FileInfo> Files { get; set; } = new ObservableCollection<FileInfo>();

        /// <summary>
        /// Gets the Nodes that are representing the subDirectory
        /// </summary>
        public ObservableCollection<FileSystemNode> Nodes { get; } = new ObservableCollection<FileSystemNode>();

        /// <summary>
        /// Search for the files that are matching with the given pattern, in the given root directory (recursive search)
        /// </summary>
        /// <param name="rootDir">Root directory for the recursive search</param>
        /// <param name="pattern">pattern to search for</param>
        /// <returns>A list of FileInfo objects with the search results</returns>
        public List<FileInfo> SearchFile(DirectoryInfo rootDir, string pattern)
        {
            return _fileSeeker.Search(rootDir, pattern);
        }

        /// <summary>
        /// Compresses given files to a given destination.
        /// </summary>
        /// <param name="files">Collection</param>
        /// <param name="destination">DirectoryInfo object</param>
        public void CompressFiles(List<FileInfo> files, DirectoryInfo destination)
        {
            foreach (var file in files)
            {
                _fileArchiver.Compress(file, destination);
            }
        }

        /// <summary>
        /// kekfgdfgfgfdgfdgfdgfdg
        /// </summary>
        /// <param name="file">lol</param>
        /// <param name="dir">llol</param>
        public void HandleFileAction(FileInfo file, DirectoryInfo dir)
        {
            _handlerDelegate.Invoke(file, dir);
        }

        /// <summary>
        /// sets the class delegate function to Copy
        /// </summary>
        public void SetDelegateToCopy()
        {
            _handlerDelegate = FileHandler.Copy;
        }

        /// <summary>
        /// sets the class delegate to move
        /// </summary>
        public void SetDelegateToMove()
        {
            _handlerDelegate = FileHandler.Move;
        }

        /// <summary>
        /// Deletes passed files.
        /// </summary>
        /// <param name="clipBoard">Clipboard with the files to delete</param>
        public void DeleteFiles(List<FileInfo> clipBoard)
        {
            foreach (var file in clipBoard)
            {
                FileHandler.DeleteFile(file);
            }
        }

        /// <summary>
        /// changes the attribute of a file
        /// </summary>
        /// <param name="file">file to change</param>
        /// <param name="dest">destination directory</param>
        /// <param name="fileName">new filename</param>
        /// <param name="extension">new extension</param>
        /// <param name="visibility">visibility</param>
        public void ChangeAttributes(FileInfo file, DirectoryInfo dest, string fileName, string extension, string visibility)
        {
            FileHandler.ModifyVisibility(file, visibility);
            FileHandler.Move(file, dest, fileName, extension);
        }
    }
}
