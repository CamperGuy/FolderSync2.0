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
        Program program = new Program();
        private List<Syncer> modules = new List<Syncer>();
        private string settingsLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + @"\Foldersync\Settings\";

        public MainMenu()
        {
            
        }

        public void loadMenus()
        {
            if (Directory.Exists(settingsLocation) && Directory.GetFiles(settingsLocation).Count() != 0)
            {
                int amount = 0;
                int increaser = 0;
                foreach (var file in Directory.EnumerateFiles(settingsLocation, "*.setting"))
                {
                    string name = File.ReadAllLines(file).ElementAtOrDefault(0);
                    string localpath = File.ReadAllLines(file).ElementAtOrDefault(1);
                    string remotepath = File.ReadAllLines(file).ElementAtOrDefault(2);

                    modules.Add(new Syncer(name, localpath, remotepath));
                    amount++;
                }
                foreach (Syncer syncer in modules)
                {
                    if (increaser <= amount)
                    {
                        Console.WriteLine("[" + increaser + "] " + modules[increaser].getName());
                        increaser++;
                    }
                    else
                        break;
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Empty\n\n[Escape] Exit\n[Any] Create a new Link");
                Console.Write("\n>");
                if (Console.ReadKey().Key != ConsoleKey.Escape)
                {
                    Syncer name = new Syncer("", "", "");
                    Console.Clear();
                    name.firstTimeSetup();
                }
                else
                    Environment.Exit(0);
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
