using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMVC.Models
{
    public class Item
    {
        public Item()
        {
            Folder1 = new FolderCompareDetails();

            Folder2 = new FolderCompareDetails();
        }

        public FolderCompareDetails Folder1 { get; set; }

        public FolderCompareDetails Folder2 { get; set; }
    }
}