using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsDesktop;

namespace WebSearchDeskBand
{
    public static class Tools
    {
        public static Config Config { get; set; }
        public static string GetDesktopName(VirtualDesktop desktop) => GetDesktopName(desktop.Id);
        public static string GetDesktopName(Guid desktopId) => Config.DesktopNames.ContainsKey(desktopId) ? Config.DesktopNames[desktopId] : $"Desktop {VirtualDesktop.GetDesktops().ToList().FindIndex(x => x.Id == desktopId) + 1}";
    }
}
