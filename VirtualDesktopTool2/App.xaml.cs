using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WindowsDesktop;

namespace VirtualDesktopTool2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0 && uint.TryParse(e.Args[0], out uint desktopNum))
            {
                if (!(VirtualDesktop.GetDesktops().GetValue(desktopNum - 1) is VirtualDesktop desktop))
                {
                    Console.WriteLine("Invalid desktop number");
                    Current.Shutdown(1);
                    return;
                }

                desktop.Switch();
                Current.Shutdown(0);
                return;
            }

            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
