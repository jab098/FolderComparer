using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new Item());
        }
        public ActionResult Compare(string folder1, string folder2)
        {
            Item item = new Item();
            Managers.FolderComparer fc = new Managers.FolderComparer();
            var result = fc.Compare(folder1, folder2);

            item.Folder1 = result[0];
            item.Folder2 = result[1];
            return View(item);
        }
    }
}