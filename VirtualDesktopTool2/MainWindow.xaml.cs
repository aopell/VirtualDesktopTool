using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsDesktop;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace VirtualDesktopTool2
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
            VirtualDesktop.Created += UpdateToolbar;
        }

        private void UpdateToolbar(object sender, VirtualDesktop e)
        {

        }

        private void SelectToolbarFolder(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.Description = "Create an empty folder which will be used to create the taskbar toolbar";
            folderBrowserDialog.ShowDialog();

            if (Directory.GetFiles(folderBrowserDialog.SelectedPath).Length > 0)
            {
                MessageBox.Show("Please select an empty folder");
            }

            FolderPathLabel.Content = folderBrowserDialog.SelectedPath;
        }
    }
}
