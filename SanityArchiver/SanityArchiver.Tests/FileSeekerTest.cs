using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SanityArchiver.Application.Models;

namespace SanityArchiver.Tests
{
    /// <summary>
    /// Test for FileSeeker.Search method
    /// </summary>
    [TestClass]
    public class FileSeekerTest
    {
        private FileSeeker _fileSeeker;

        [TestInitialize]
        public void initTest()
        {
            _fileSeeker = new FileSeeker();
        }

        [TestMethod]
        public void GetFileForSearch()
        {
            string path = @"C:\Users\sfarkas\Desktop\CSharp_Udemy";
            string fileName = "Loops.cs";
            FileInfo[] files = new DirectoryInfo(path).GetFiles(fileName);
            var result = _fileSeeker.Search(path, fileName);
            Assert.IsTrue(result.Equals(files));
        }

        [TestMethod]
        public void GetFileForRecursiveSearch()
        {
            string path = @"C:\Users\sfarkas\Desktop";
            string fileName = "Loops.cs";
            var result = _fileSeeker.Search(path,fileName);
            Assert.IsNotNull(result);
        }
    }
}
