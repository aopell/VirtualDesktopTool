using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

namespace VirtualDesktopTool2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<VirtualDesktopControl> DesktopControls { get; } = new List<VirtualDesktopControl>();

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

            DesktopControls.Add(Desktop1);
            DesktopControls.Add(Desktop2);
            DesktopControls.Add(Desktop3);
            DesktopControls.Add(Desktop4);
            DesktopControls.Add(Desktop5);
            DesktopControls.Add(Desktop6);
            DesktopControls.Add(Desktop7);
            DesktopControls.Add(Desktop8);
            DesktopControls.Add(Desktop9);
            DesktopControls.Add(Desktop10);

            VirtualDesktop.Created += VirtualDesktop_Created;
            VirtualDesktop.Destroyed += VirtualDesktop_Destroyed;
            VirtualDesktop.CurrentChanged += VirtualDesktop_CurrentChanged;
            VirtualDesktop.Renamed += VirtualDesktop_Renamed;

            Timer timer = new Timer(100);
            timer.Elapsed += async (ss, ee) => Dispatcher.Invoke(UpdateToolbar);
            timer.Start();

            UpdateToolbar();
        }

        private void VirtualDesktop_Renamed(object sender, VirtualDesktopRenamedEventArgs e) => UpdateToolbar();
        private void VirtualDesktop_CurrentChanged(object sender, VirtualDesktopChangedEventArgs e) => UpdateToolbar();
        private void VirtualDesktop_Destroyed(object sender, VirtualDesktopDestroyEventArgs e) => UpdateToolbar();
        private void VirtualDesktop_Created(object sender, VirtualDesktop e) => UpdateToolbar();

        private void UpdateToolbar()
        {
            var desktops = VirtualDesktop.GetDesktops();
            var nums = Enumerable.Range(1, desktops.Length).ToArray();

            foreach (int n in nums)
            {
                DesktopControls[n - 1].UpdateLayout();

                Tools.CreateShortcut($"{n} - {DesktopControls[n - 1].DesktopName.Text}",
                                     Path.GetFullPath(Tools.ToolbarDirectory),
                                     "dotnet.exe",
                                     $"\"{Assembly.GetExecutingAssembly().Location}\" {n}",
                                     Tools.WindowStyle.Minimized,
                                     DesktopControls[n - 1].DesktopName.Text,
                                     Path.GetFullPath(DesktopControls[n - 1].ImagePath)
                );
            }

            int current = Array.IndexOf(desktops, VirtualDesktop.Current) + 1;
            Tools.CreateShortcut($"{current} - {DesktopControls[current - 1].DesktopName.Text}",
                                 Path.GetFullPath(Tools.ToolbarDirectory),
                                 "dotnet.exe",
                                 $"\"{Assembly.GetExecutingAssembly().Location}\" {current}",
                                 Tools.WindowStyle.Minimized,
                                 DesktopControls[current - 1].DesktopName.Text,
                                 Path.GetFullPath(DesktopControls[current - 1].SelectedImagePath)
            );

            bool deleted = false;
            var keepFiles = nums.Select(n => $"{n} - {desktops[n - 1].Name}.lnk").ToList();
            foreach (string file in Directory.GetFiles(Tools.ToolbarDirectory).Where(f => !keepFiles.Contains(Path.GetFileName(f))))
            {
                deleted = true;
                File.Delete(file);
            }

            if (deleted)
            {
                // Hacky solution for getting the taskbar toolbar to update when the shortcut files are deleted
                var tempPath = Path.Combine(Tools.ToolbarDirectory, ".vdesktop-temp");
                File.Create(tempPath).Close();
                Task.Delay(100).ContinueWith(_ => File.Delete(tempPath));
            }
        }

        private void SelectToolbarFolder(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", Path.GetFullPath(Tools.ToolbarDirectory));
        }
    }
}
