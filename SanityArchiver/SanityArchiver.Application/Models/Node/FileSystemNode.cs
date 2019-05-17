using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.IO;

namespace SanityArchiver.Application.Models.Node
{
    /// <summary>
    /// The instances of this class represent the directories of the file system
    /// </summary>
    public class FileSystemNode
    {
        private bool _isExpanded;
        private DispatcherTimer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemNode"/> class.
        /// Its instances represent one directory in the TreeView
        /// </summary>
        /// <param name="dir">DirectoryInfo object of the represented directory</param>
        public FileSystemNode(DirectoryInfo dir)
        {
            Dir = dir;
        }

        /// <summary>
        /// Gets the the DirectoryInfo object of the represented directory
        /// </summary>
        public DirectoryInfo Dir { get; }

        /// <summary>
        /// Gets the name of the represented directory
        /// </summary>
        public string Name => Dir.Name;

        /// <summary>
        /// Gets the path of the represented directory
        /// </summary>
        public string Path => Dir.FullName;

        /// <summary>
        /// Gets the collection nodes of the subDirectories
        /// </summary>
        public ObservableCollection<FileSystemNode> Nodes { get; } = new ObservableCollection<FileSystemNode>();

        /// <summary>
        /// Gets the collection of the files contained by the directory (this node)
        /// </summary>
        public ObservableCollection<FileInfo> Files { get; } = new ObservableCollection<FileInfo>();

        /// <summary>
        /// Gets or sets a value indicating whether the node is expanded or not.
        /// </summary>
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                if (_isExpanded)
                {
                    foreach (var node in Nodes)
                    {
                        node.LoadContent();
                    }
                }
            }
        }

        /// <summary>
        /// Start a timer to refresh the node content periodically.
        /// </summary>
        public void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.5);
            _timer.Tick += (sender, args) => RefreshFiles();
            _timer.Start();
        }

        /// <summary>
        /// Stop a timer of the node.
        /// </summary>
        public void StopTimer()
        {
            _timer.Stop();
        }

        /// <summary>
        /// Loads the content (subDirectories and files), on expanding the representing TreeViewItem
        /// </summary>
        public void LoadContent()
        {
            LoadFiles();
            LoadSubDirs();
        }

        /// <summary>
        /// Refreshes the contained files, syncs old and new collection to refresh the TreeView
        /// </summary>
        private void RefreshFiles()
        {
            try
            {
                var newFiles = Dir.GetFiles();
                var filesToHandle = SetOfFileDifferences(Files, newFiles);
                foreach (var file in filesToHandle)
                {
                    Files.Remove(file);
                }

                filesToHandle = SetOfFileDifferences(newFiles, Files);
                foreach (var file in filesToHandle)
                {
                    Files.Add(file);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        /// <summary>
        /// Creates the set of differences between two file sets
        /// </summary>
        /// <param name="set1">FileInfo collection subtract from</param>
        /// <param name="set2">FileInfo collection to subtract</param>
        /// <returns>The set of differences (FileInfo collection) to handle in RefreshFiles method</returns>
        private List<FileInfo> SetOfFileDifferences(ICollection<FileInfo> set1, ICollection<FileInfo> set2)
        {
            var setOfDifferences = new List<FileInfo>();
            foreach (var file1 in set1)
            {
                var contains = false;
                foreach (var file2 in set2)
                {
                    if (file1.Name.Equals(file2.Name))
                    {
                        contains = true;
                    }
                }

                if (!contains)
                {
                    setOfDifferences.Add(file1);
                }
            }

            return setOfDifferences;
        }

        /// <summary>
        /// Loads the subDirectories (nodes)
        /// </summary>
        private void LoadSubDirs()
        {
            Nodes.Clear();
            try
            {
                foreach (var subDir in Dir.GetDirectories())
                {
                    Nodes.Add(new FileSystemNode(subDir));
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Loads the contained files
        /// </summary>
        private void LoadFiles()
        {
            Files.Clear();
            try
            {
                foreach (var file in Dir.GetFiles())
                {
                    Files.Add(file);
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
