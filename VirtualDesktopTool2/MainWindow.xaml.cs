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
            if (!Directory.Exists(Tools.ToolbarDirectory))
            {
                Directory.CreateDirectory(Tools.ToolbarDirectory);
            }

            VirtualDesktop.Created += VirtualDesktop_Created;
            VirtualDesktop.Destroyed += VirtualDesktop_Destroyed;
            VirtualDesktop.CurrentChanged += VirtualDesktop_CurrentChanged;

            UpdateToolbar();

            Tools.CreateShortcut("Test",
                                 "D:\\aaron\\Documents\\GitHub\\VirtualDesktopTool\\VirtualDesktopTool2\\bin\\Release\\netcoreapp3.1\\Virtual Desktops",
                                 "notepad.exe",
                                 "",
                                 Tools.WindowStyle.Normal,
                                 "Test thing",
                                 @"D:\aaron\Documents\GitHub\VirtualDesktopTool\VirtualDesktopTool2\bin\Release\netcoreapp3.1\Icons\desktop.ico"
            );
        }

        private void VirtualDesktop_CurrentChanged(object sender, VirtualDesktopChangedEventArgs e) => UpdateToolbar();
        private void VirtualDesktop_Destroyed(object sender, VirtualDesktopDestroyEventArgs e) => UpdateToolbar();
        private void VirtualDesktop_Created(object sender, VirtualDesktop e) => UpdateToolbar();

        private void UpdateToolbar()
        {
            var desktops = VirtualDesktop.GetDesktops();
            var nums = Enumerable.Range(1, desktops.Length).ToArray();

            foreach (int n in nums)
            {
                Tools.CreateShortcut(n.ToString(),
                                     Path.GetFullPath(Tools.ToolbarDirectory),
                                     "dotnet.exe",
                                     $"\"{Assembly.GetExecutingAssembly().Location}\" {n}",
                                     Tools.WindowStyle.Minimized,
                                     $"Desktop {n}",
                                     Path.GetFullPath($"Icons\\{(n <= 10 ? n.ToString() : "desktop")}.ico")
                );
            }

            var keepFiles = nums.Select(n => $"{n}.lnk").ToList();
            foreach (string file in Directory.GetFiles(Tools.ToolbarDirectory).Where(f => !keepFiles.Contains(Path.GetFileName(f))))
            {
                File.Delete(file);
            }
        }

        private void SelectToolbarFolder(object sender, RoutedEventArgs e)
        {

        }
    }
}
