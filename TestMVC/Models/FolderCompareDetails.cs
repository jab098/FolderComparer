using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMVC.Models
{
    public class FolderCompareDetails
    {
        public FolderCompareDetails()
        {
            FileDetails = new List<FileDetails>();
        }

        public string Name { get; set; }
        public List<FileDetails> FileDetails { get; set; }
    }
}