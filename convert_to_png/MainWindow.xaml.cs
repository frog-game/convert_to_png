using ConvertToPng;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;

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

   
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Helper_FlieLogic.createTaskProgress(this);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                if (Path.GetFileNameWithoutExtension(dialog.FileName) == "max")
                {
                    Helper_FlieLogic.start3dMaxProcess(dialog.FileName);
                }

                else if (Path.GetFileNameWithoutExtension(dialog.FileName) == "blend")
                {
                    Helper_FlieLogic.startBlendProcess(dialog.FileName);
                }
                else
                {
                    Helper_FlieLogic.startAllProcess(dialog.FileName);
                }
            }
        }
    }
}
