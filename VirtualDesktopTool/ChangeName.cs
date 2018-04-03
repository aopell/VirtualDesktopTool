using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

namespace WebSearchDeskBand
{
    public partial class ChangeName : Form
    {
        private readonly VirtualDesktop desktop;
        public ChangeName()
        {
            desktop = VirtualDesktop.Current;
            InitializeComponent();
        }

        private void ChangeName_Load(object sender, EventArgs e)
        {
            textBox1.Text = Tools.GetDesktopName(desktop);
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tools.Config.DesktopNames[desktop.Id] = string.IsNullOrWhiteSpace(textBox1.Text) ? Tools.GetDesktopName(desktop) : textBox1.Text;
            Tools.Config.Save();
            Close();
        }
    }
}
