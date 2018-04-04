using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

namespace WebSearchDeskBand
{
    public partial class DeskBandUI : UserControl
    {
        public DeskBandUI()
        {
            Config.Load();
            InitializeComponent();
        }

        private void DeskBandUI_Load(object sender, EventArgs e)
        {
            desktopLabel.MouseWheel += DesktopLabel_MouseWheel;
            VirtualDesktop.CurrentChanged += VirtualDesktop_CurrentChanged;
            VirtualDesktop.Destroyed += VirtualDesktop_Destroyed;
            VirtualDesktop.Created += VirtualDesktop_Created;
            UpdateDisplay(VirtualDesktop.Current);
        }

        private void DesktopLabel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                (VirtualDesktop.Current.GetRight() ?? VirtualDesktop.GetDesktops()[0]).Switch();
            }
            else if (e.Delta < 0)
            {
                (VirtualDesktop.Current.GetLeft() ?? VirtualDesktop.GetDesktops().Last()).Switch();
            }
        }

        private void VirtualDesktop_Created(object sender, VirtualDesktop e)
        {
            UpdateDisplay(e);
        }

        private void VirtualDesktop_Destroyed(object sender, VirtualDesktopDestroyEventArgs e)
        {
            UpdateDisplay(e.Fallback);
        }

        private void VirtualDesktop_CurrentChanged(object sender, VirtualDesktopChangedEventArgs e)
        {
            UpdateDisplay(e.NewDesktop);
        }

        private void UpdateDisplay(VirtualDesktop v)
        {
            desktopLabel.Text = Tools.GetDesktopName(v);
            ScaleFont(desktopLabel);
        }

        private static void ScaleFont(Control label)
        {
            SizeF extent = TextRenderer.MeasureText(label.Text, label.Font);

            float hRatio = label.Height / extent.Height;
            float wRatio = label.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = label.Font.Size * ratio;

            label.Font = new Font(label.Font.FontFamily, Math.Min(newSize, 12.5f), label.Font.Style);

        }

        private void desktopLabel_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                case MouseButtons.Right:
                    new ChangeName().ShowDialog();
                    UpdateDisplay(VirtualDesktop.Current);
                    break;
                case MouseButtons.Middle:
                    VirtualDesktop.Current.Remove();
                    break;
                case MouseButtons.XButton1:
                    (VirtualDesktop.Current.GetLeft() ?? VirtualDesktop.GetDesktops().Last()).Switch();
                    break;
                case MouseButtons.XButton2:
                    (VirtualDesktop.Current.GetRight() ?? VirtualDesktop.GetDesktops()[0]).Switch();
                    break;
            }
        }
    }
}
