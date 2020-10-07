using System;
using System.Collections.Generic;
using System.Text;
using IWshRuntimeLibrary;

namespace VirtualDesktopTool2
{
    public static class Tools
    {
        public static void CreateShortcut(string name, string directory, string target, string arguments, WindowStyle style, string description, string iconPath)
        {
            string shortcutLocation = System.IO.Path.Combine(directory, name + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.WindowStyle = (int)style;
            shortcut.Description = description;
            shortcut.IconLocation = iconPath;
            shortcut.TargetPath = target;
            shortcut.Arguments = arguments;
            shortcut.Save();
        }

        public enum WindowStyle
        {
            Normal = 1,
            Maximized = 3,
            Minimized = 7
        }
    }
}
