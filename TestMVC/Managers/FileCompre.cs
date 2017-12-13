using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

    class FileCompare : IEqualityComparer<System.IO.FileInfo>
    {
        public FileCompare() { }

        public bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
        {
            bool isContentSame = f1.Name == f2.Name && f1.Length == f2.Length;

            if (isContentSame)
            {
                using (var md5 = MD5.Create())
                {
                    byte[] a, b;

                    using (var stream = f1.Open(System.IO.FileMode.Open))
                    {
                        a = md5.ComputeHash(stream);
                    }

                    using (var stream = f2.Open(System.IO.FileMode.Open))
                    {
                        b = md5.ComputeHash(stream);
                    }

                    isContentSame = Encoding.UTF8.GetString(a) == Encoding.UTF8.GetString(b);
                }
            }

            return isContentSame;
        }
        public int GetHashCode(System.IO.FileInfo fi)
        {
            string s = String.Format("{0}{1}", fi.Name, fi.Length);
            return s.GetHashCode();
        }
    }
