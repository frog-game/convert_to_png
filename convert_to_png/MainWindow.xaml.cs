using Microsoft.Win32;
using NPOI.HPSF;
using OpenMcdf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using ConvertToPng;

namespace convert_to_png
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                Helper_3dFile.loadAll3dMaxFile(dialog.FileName);
                Helper_3dFile.all3dMaxFileConvertToPng();

            }

        }
    }
}
