using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CSDeskBand;
using CSDeskBand.ContextMenu;

namespace VirtualDesktopTool
{
    [ComVisible(true)]
    [Guid("17F380B7-BE3B-4900-B02F-8137ABACC6F0")]
    [CSDeskBandRegistration(Name = "Virtual Desktop Tool")]
    public class DeskBand : CSDeskBandWpf
    {
        protected override UIElement UIElement => new DeskBandControl();

        public DeskBand()
        {

        }
    }
}
