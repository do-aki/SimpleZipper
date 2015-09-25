using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleZipper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void changeStatus(int parcent, string status)
        {
            progressStatus.Value = parcent;
            textStatus.Text = status;
        }

        private async Task archiveDirectories(string[] dirs)
        {
            int complete = 0;

            foreach (var f in dirs)
            {
                int parcent = (int)(100 * ((float)complete++ / dirs.Length));
                changeStatus(parcent, "Compressing " + Path.GetFileName(f));
                await Task.Run(() => Archive.ZipDirectory(f));
            }
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
        }

        private async void Window_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var dirs = Archive.extractDirectory((string[])e.Data.GetData(DataFormats.FileDrop));

            changeStatus(0, "Busy");
            await this.archiveDirectories(dirs);
            changeStatus(100, "Waiting...");
        }
    }
}
