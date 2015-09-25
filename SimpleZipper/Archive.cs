using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;

namespace SimpleZipper
{
    class Archive
    {
        public static void ZipDirectory(string dir)
        {
            string zipfile_name = dir + ".zip";
            if (File.Exists(zipfile_name))
            {
                return;
            }

            using (var zip = new ZipFile(Encoding.GetEncoding("shift_jis")))
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                var di = new DirectoryInfo(dir);

                foreach (var i in di.EnumerateFileSystemInfos())
                {
                    if (FileAttributes.Directory == (i.Attributes & FileAttributes.Directory))
                    {
                        zip.AddDirectory(i.FullName, i.Name);
                    }
                    else if (FileAttributes.Hidden != (i.Attributes & FileAttributes.Hidden))
                    {
                        zip.AddFile(i.FullName, "");
                    }
                }

                zip.Save(zipfile_name);
            }
        }

        public static string[] extractDirectory(string[] files)
        {
            List<string> dirs = new List<string>();
            foreach (var f in files)
            {
                if (Directory.Exists(f))
                {
                    dirs.Add(f);
                }
            }

            return dirs.ToArray();
        }
    }
}
