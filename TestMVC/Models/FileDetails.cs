using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMVC.Models
{
    public class FileDetails
    {
        public FileDetails()
        {
            SameContentFiles = new List<string>();
        }

        public string Name { get; set; }

        public bool ExistsBothFolder { get; set; }

        public bool IsDateModifiedSame { get; set; }

        public bool IsContentSame { get; set; }

        public bool HasSameContentWithAnotherFileName
        {
            get { return SameContentFiles.Count > 0; }
        }

        public List<string> SameContentFiles { get; set; }

        public bool IsFolder { get; set; }

        //Sets the background colour based on the result of file
        public string BackgroundColour()
        {
            string colour = string.Empty;
            if (ExistsBothFolder && IsContentSame)
            {
                colour = "lightgreen";
            }
            else if (!HasSameContentWithAnotherFileName && !ExistsBothFolder && !IsContentSame && !IsDateModifiedSame)
            {
                colour = "lightcoral";
            }
            else if (ExistsBothFolder && !IsContentSame)
            {
                colour = "lightskyblue";
            }
            else if (HasSameContentWithAnotherFileName && !ExistsBothFolder)
            {
                colour = "orange";
            }
            return colour;
        }
    }
}