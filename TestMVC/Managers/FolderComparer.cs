using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TestMVC.Models;

namespace TestMVC.Managers
{
    public class FolderComparer
    {
        //Main compare function that goes through all other functions
        public List<FolderCompareDetails> Compare(string folder1, string folder2)
        {
            List<FolderCompareDetails> result = new List<FolderCompareDetails>();
            result.Add(new FolderCompareDetails { Name = folder1, FileDetails = GetFolderDetails(folder1, folder2) });
            result.Add(new FolderCompareDetails { Name = folder2, FileDetails = GetFolderDetails(folder2, folder1) });
            return result;
        }

        //Gets generic info about file
        private List<FileDetails> GetFolderDetails(string folder1, string folder2)
        {
            List<FileDetails> folder1Differences = new List<FileDetails>();
            if ((Directory.Exists(folder1)) && (Directory.Exists(folder2)))
            {
                //List<FileDetails> folder1Differences = new List<FileDetails>();
                List<string> folderFiles = Directory.EnumerateFileSystemEntries(folder1).ToList();

                for (int i = 0; i < folderFiles.Count(); i++)
                {
                    string fullAPath = Path.Combine(folder1, Path.GetFileName(folderFiles[i]));
                    string fullBPath = Path.Combine(folder2, Path.GetFileName(folderFiles[i]));

                    FileDetails a = new FileDetails();

                    //a.IsFolder = Directory.Exists(fullAPath);

                    a.Name = Path.GetFileName(fullAPath);

                    if (Directory.Exists(fullAPath))
                    {
                        a.ExistsBothFolder = Directory.Exists(Path.Combine(folder2, a.Name));
                        a.IsFolder = IsFolder(fullAPath, fullBPath);
                    }
                    else
                    {
                        a.ExistsBothFolder = File.Exists(Path.Combine(folder2, a.Name));
                    }

                    a.SameContentFiles = CheckFileContentWhichMatch(folderFiles[i], folder2);

                    if (a.ExistsBothFolder)
                    {
                        a.IsDateModifiedSame = File.GetLastWriteTime(fullAPath) == File.GetLastWriteTime(fullBPath);
                        a.IsContentSame = IsContentSame(fullAPath, fullBPath);
                    }

                    folder1Differences.Add(a);
                }
            }
            else
            {
                //List<FileDetails> folder1Differences = new List<FileDetails>();
                FileDetails a = new FileDetails();
                a.Name = "DirectoryNotFound";
                folder1Differences.Add(a);
            }

            return folder1Differences;
        }

        //Method checks to see if file is same to other directory files
        private bool IsContentSame(string file1, string file2)
        {
            bool isContentSame = false;

            if (Directory.Exists(file1) && Directory.Exists(file2))
            {
                DirectoryInfo dir1 = new DirectoryInfo(file1);
                DirectoryInfo dir2 = new DirectoryInfo(file2);

                IEnumerable<FileInfo> list1 = dir1.GetFiles("*.*", SearchOption.AllDirectories);
                IEnumerable<FileInfo> list2 = dir2.GetFiles("*.*", SearchOption.AllDirectories);

                FileCompare myFileCompare = new FileCompare();

                isContentSame = list1.SequenceEqual(list2, myFileCompare);
            }
            else if (Path.HasExtension(file1))
            {
                using (var md5 = MD5.Create())
                {
                    byte[] a, b;

                    using (var stream = File.OpenRead(file1))
                    {
                        a = md5.ComputeHash(stream);
                    }

                    using (var stream = File.OpenRead(file2))
                    {
                        b = md5.ComputeHash(stream);
                    }

                    isContentSame = Encoding.UTF8.GetString(a) == Encoding.UTF8.GetString(b);
                }
            }

            return isContentSame;
        }

        //Method checks if item is a folder or file
        private bool IsFolder(string file1, string file2)
        {
            bool isFolder = false;
            FileAttributes attr = File.GetAttributes(file1);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFolder = true;
            }

            return isFolder;
        }

        //Method checks if the content inside file/folder match
        private List<string> CheckFileContentWhichMatch(string file1, string folder)
        {
            List<string> fileNames = new List<string>();

            if (File.Exists(file1))
            {
                using (var md5 = MD5.Create())
                {
                    byte[] a, b;

                    using (var stream = File.OpenRead(file1))
                    {
                        a = md5.ComputeHash(stream);
                    }

                    foreach (var item in Directory.GetFiles(folder))
                    {
                        if (!Path.GetFileName(item).Equals(Path.GetFileName(file1), StringComparison.CurrentCultureIgnoreCase))
                        {
                            using (var stream = File.OpenRead(item))
                            {
                                b = md5.ComputeHash(stream);
                            }


                            if (Encoding.UTF8.GetString(a) == Encoding.UTF8.GetString(b))
                            {
                                fileNames.Add(item);
                            }
                        }
                    }
                }
            }

            return fileNames;
        }
    }
}