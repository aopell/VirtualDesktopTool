using SharpShell.Attributes;
using SharpShell.SharpDeskBand;
using System;
using System.Runtime.InteropServices;

namespace WebSearchDeskBand
{

    [ComVisible(true)]
    [DisplayName("Virtual Desktop")]
    [Guid("095CF383-6240-4524-B16D-B478577758FC")]
    public class VirtualDesktopDeskBand : SharpDeskBand
    {
        protected override System.Windows.Forms.UserControl CreateDeskBand()
        {
            return new DeskBandUI();
        }

        protected override BandOptions GetBandOptions()
        {
            return new BandOptions
            {
                HasVariableHeight = false,
                IsSunken = false,
                ShowTitle = true,
                Title = "Virtual Desktop",
                UseBackgroundColour = true,
                AlwaysShowGripper = true
            };
        }

        [ComRegisterFunction]
        public static void RegisterClass(Type type) => ComUtilities.RegisterDeskBandClass(type);

        [ComUnregisterFunction]
        public static void UnregisterClass(Type type) => ComUtilities.UnregisterClass(type);
    }
}
