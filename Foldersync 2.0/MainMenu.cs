using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Foldersync
{
    class MainMenu
    {
        private Program program = new Program();
        private List<Syncer> modules = new List<Syncer>();
        private string settingsLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + @"\Foldersync\Settings\";

        public MainMenu()
        {
            loadMenus();
        }

        private void loadMenus()
        {
            foreach (var file in Directory.EnumerateFiles(settingsLocation, "*.setting"))
            {
                string name = File.ReadAllLines(file).ElementAtOrDefault(0);
                string localpath = File.ReadAllLines(file).ElementAtOrDefault(1);
                string remotepath = File.ReadAllLines(file).ElementAtOrDefault(2);

                modules.Add(new Syncer(name, localpath, remotepath));
            }
        }

        private void openMenu()
        {

        }

        public void closeMenu(Syncer sync)
        {

        }
        public void removeMenu(Syncer sync)
        {

        }

    }
}
