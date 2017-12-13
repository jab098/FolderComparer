using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMVC.Managers;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class FolderComparerUnitTests
    {
        [TestMethod]
        public void CompareBothDirectoryWithSameContent()
        {
            FolderComparer comparer = new FolderComparer();

            var dirTemp = Directory.CreateDirectory("temp");

            var dirA = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "A"));
            var dirB = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "B"));

            File.WriteAllText(Path.Combine(dirA.FullName, "testfile.txt"), "CompareBothDirectoryWithSameContent");

            DirectoryCopy(dirA.FullName, dirB.FullName);

            var result = comparer.Compare(dirA.FullName, dirB.FullName);
            
            dirTemp.Delete(true);

            Assert.AreEqual(result[0].FileDetails.Count, result[1].FileDetails.Count);

            Assert.IsTrue(result[0].FileDetails[0].ExistsBothFolder);
            Assert.IsTrue(result[1].FileDetails[0].ExistsBothFolder);
            
            Assert.IsFalse(result[0].FileDetails[0].HasSameContentWithAnotherFileName);
            Assert.IsFalse(result[1].FileDetails[0].HasSameContentWithAnotherFileName);

            Assert.IsTrue(result[0].FileDetails[0].IsContentSame);
            Assert.IsTrue(result[1].FileDetails[0].IsContentSame);

            Assert.IsTrue(result[0].FileDetails[0].IsDateModifiedSame);
            Assert.IsTrue(result[1].FileDetails[0].IsDateModifiedSame);

            Assert.IsFalse(result[0].FileDetails[0].IsFolder);
            Assert.IsFalse(result[1].FileDetails[0].IsFolder);
        }

        [TestMethod]
        public void CompareBothDirectoryWithDifferentContent()
        {
            FolderComparer comparer = new FolderComparer();

            var dirTemp = Directory.CreateDirectory("temp");

            var dirA = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "A"));
            var dirB = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "B"));

            File.WriteAllText(Path.Combine(dirA.FullName, "testfile.txt"), "CompareBothDirectoryWithDifferentContent1");
            File.WriteAllText(Path.Combine(dirB.FullName, "testfile.txt"), "CompareBothDirectoryWithDifferentContent2");

            var result = comparer.Compare(dirA.FullName, dirB.FullName);

            dirTemp.Delete(true);

            Assert.AreEqual(result[0].FileDetails.Count, result[1].FileDetails.Count);

            Assert.IsTrue(result[0].FileDetails[0].ExistsBothFolder);
            Assert.IsTrue(result[1].FileDetails[0].ExistsBothFolder);

            Assert.IsFalse(result[0].FileDetails[0].HasSameContentWithAnotherFileName);
            Assert.IsFalse(result[1].FileDetails[0].HasSameContentWithAnotherFileName);

            Assert.IsFalse(result[0].FileDetails[0].IsContentSame);
            Assert.IsFalse(result[1].FileDetails[0].IsContentSame);

            Assert.IsFalse(result[0].FileDetails[0].IsDateModifiedSame);
            Assert.IsFalse(result[1].FileDetails[0].IsDateModifiedSame);

            Assert.IsFalse(result[0].FileDetails[0].IsFolder);
            Assert.IsFalse(result[1].FileDetails[0].IsFolder);
        }

        [TestMethod]
        public void CompareBothDirectoryWithSameContentWithSubFolders()
        {
            FolderComparer comparer = new FolderComparer();

            var dirTemp = Directory.CreateDirectory("temp");

            var dirA = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "A"));
            var dirB = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "B"));
            var dirASub = Directory.CreateDirectory(Path.Combine(dirA.FullName, "Folder"));
            var dirBSub = Directory.CreateDirectory(Path.Combine(dirB.FullName, "Folder"));

            File.WriteAllText(Path.Combine(dirASub.FullName, "testfile.txt"), "CompareBothDirectoryWithDifferentContent1");
            File.WriteAllText(Path.Combine(dirBSub.FullName, "testfile.txt"), "CompareBothDirectoryWithDifferentContent1");

            var result = comparer.Compare(dirA.FullName, dirB.FullName);

            dirTemp.Delete(true);

            Assert.AreEqual(result[0].FileDetails.Count, result[1].FileDetails.Count);

            Assert.IsTrue(result[0].FileDetails[0].ExistsBothFolder);
            Assert.IsTrue(result[1].FileDetails[0].ExistsBothFolder);

            Assert.IsFalse(result[0].FileDetails[0].HasSameContentWithAnotherFileName);
            Assert.IsFalse(result[1].FileDetails[0].HasSameContentWithAnotherFileName);

            Assert.IsTrue(result[0].FileDetails[0].IsContentSame);
            Assert.IsTrue(result[1].FileDetails[0].IsContentSame);

            Assert.IsFalse(result[0].FileDetails[0].IsDateModifiedSame);
            Assert.IsFalse(result[1].FileDetails[0].IsDateModifiedSame);

            Assert.IsTrue(result[0].FileDetails[0].IsFolder);
            Assert.IsTrue(result[1].FileDetails[0].IsFolder);
        }

        [TestMethod]
        public void CompareBothDirectoryWithDifferntContentWithSubFolders()
        {
            FolderComparer comparer = new FolderComparer();

            var dirTemp = Directory.CreateDirectory("temp");

            var dirA = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "A"));
            var dirB = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "B"));
            var dirASub = Directory.CreateDirectory(Path.Combine(dirA.FullName, "Folder1"));
            var dirBSub = Directory.CreateDirectory(Path.Combine(dirB.FullName, "Folder2"));

            File.WriteAllText(Path.Combine(dirASub.FullName, "testfile.txt"), "CompareBothDirectoryWithDifferentContent1");
            File.WriteAllText(Path.Combine(dirBSub.FullName, "testfile.txt"), "CompareBothDirectoryWithDifferentContent2");

            var result = comparer.Compare(dirA.FullName, dirB.FullName);

            dirTemp.Delete(true);

            Assert.AreEqual(result[0].FileDetails.Count, result[1].FileDetails.Count);

            Assert.IsFalse(result[0].FileDetails[0].ExistsBothFolder);
            Assert.IsFalse(result[1].FileDetails[0].ExistsBothFolder);

            Assert.IsFalse(result[0].FileDetails[0].HasSameContentWithAnotherFileName);
            Assert.IsFalse(result[1].FileDetails[0].HasSameContentWithAnotherFileName);

            Assert.IsFalse(result[0].FileDetails[0].IsContentSame);
            Assert.IsFalse(result[1].FileDetails[0].IsContentSame);

            Assert.IsFalse(result[0].FileDetails[0].IsDateModifiedSame);
            Assert.IsFalse(result[1].FileDetails[0].IsDateModifiedSame);

            Assert.IsTrue(result[0].FileDetails[0].IsFolder);
            Assert.IsTrue(result[1].FileDetails[0].IsFolder);
        }

        [TestMethod]
        public void CompareBothDirectoryWithDifferntContentWithSameSubFolderContents()
        {
            FolderComparer comparer = new FolderComparer();

            var dirTemp = Directory.CreateDirectory("temp");

            var dirA = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "A"));
            var dirB = Directory.CreateDirectory(Path.Combine(dirTemp.FullName, "B"));
            var dirASub = Directory.CreateDirectory(Path.Combine(dirA.FullName, "Folder1"));
            var dirBSub = Directory.CreateDirectory(Path.Combine(dirB.FullName, "Folder1"));

            File.WriteAllText(Path.Combine(dirASub.FullName, "testfile2.txt"), "CompareBothDirectoryWithDifferentContent2");
            File.WriteAllText(Path.Combine(dirBSub.FullName, "testfile1.txt"), "CompareBothDirectoryWithDifferentContent");

            var result = comparer.Compare(dirA.FullName, dirB.FullName);

            dirTemp.Delete(true);

            Assert.AreEqual(result[0].FileDetails.Count, result[1].FileDetails.Count);

            Assert.IsTrue(result[0].FileDetails[0].ExistsBothFolder);
            Assert.IsTrue(result[1].FileDetails[0].ExistsBothFolder);

            Assert.IsFalse(result[0].FileDetails[0].HasSameContentWithAnotherFileName);
            Assert.IsFalse(result[1].FileDetails[0].HasSameContentWithAnotherFileName);

            Assert.IsFalse(result[0].FileDetails[0].IsContentSame);
            Assert.IsFalse(result[1].FileDetails[0].IsContentSame);

            Assert.IsFalse(result[0].FileDetails[0].IsDateModifiedSame);
            Assert.IsFalse(result[1].FileDetails[0].IsDateModifiedSame);

            Assert.IsTrue(result[0].FileDetails[0].IsFolder);
            Assert.IsTrue(result[1].FileDetails[0].IsFolder);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }

        }
    }
}
