using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleZipper
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (0 < e.Args.Length)
            {
                var dirs = Archive.extractDirectory(e.Args);
                foreach (var d in dirs) { 
                    Archive.ZipDirectory(d);
                }

                Environment.Exit(0);
            }
        }
    }
}
