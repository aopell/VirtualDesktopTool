using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;
using Newtonsoft.Json;

namespace WebSearchDeskBand
{
    public class Config
    {
        private static readonly string dirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\VirtualDesktopTool\\";
        public Dictionary<Guid, string> DesktopNames { get; set; } = new Dictionary<Guid, string>();

        public void Save()
        {
            foreach (Guid g in DesktopNames.Keys.Where(x => !VirtualDesktop.GetDesktops().Select(d => d.Id).Contains(x)).ToArray())
            {
                DesktopNames.Remove(g);
            }
            File.WriteAllText(dirPath + "config.json", JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public static void Load()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                if (!File.Exists(dirPath + "config.json"))
                {
                    File.Create("config.json").Dispose();
                }

                Tools.Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(dirPath + "config.json")) ?? new Config();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
