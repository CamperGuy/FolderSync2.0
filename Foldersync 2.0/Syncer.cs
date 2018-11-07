using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions; // To use ASCII in the console for arrows
using System.Runtime.InteropServices; // to lock console

namespace Foldersync
{
    class Syncer
    {
        // Initialise other classes
        private MainMenu menu = new MainMenu();

        // Initialise fields
        private bool isFirstTimeSetup = true;
        private string localpath;
        private string remotepath;
        private string name;

        /// <summary>
        /// Creating a new instance of a FolderSync Object
        /// </summary>
        /// <param name="localpath">Give the localpath</param>
        /// <param name="remotepath">Give the remotepath</param>
        public Syncer(string name, string localpath, string remotepath)
        {
            // Initialise fields
            this.localpath = localpath;
            this.remotepath = remotepath;
            this.name = name;

            // See if this is an entirely new object and redefine fields if so
            if (name != "" && localpath != "" && remotepath != "")
                isFirstTimeSetup = false;
            else if (isFirstTimeSetup == true)
                firstTimeSetup();

            
            // Validate the fields. If any contain invalid data, prompt the user to re-enter or delete this option
            if (name.Length <= 2)
            {
                Console.WriteLine(name);
                Console.WriteLine("The given name must have at least 3 characters\nPlease rename this setting before continuing.");
                Console.Write("[Escape] Delete this setting\n[Any] To rename\n>");
                if (Console.ReadKey().Key != ConsoleKey.Escape)
                    rename();
                else
                    menu.removeMenu(this);
            }
            if (!Program.isPathValid(localpath, 0))
            {
                Console.WriteLine("The given localpath is not valid. Please re-enter it before continuing.");
                Console.Write("[Escape] Delete this setting\n[Any] Relocate Localpath\n>");
                if (Console.ReadKey().Key != ConsoleKey.Escape)
                    generateLocalPath();
                else
                    menu.removeMenu(this);
            }

            if (!Program.isPathValid(remotepath, 0))
            {
                Console.WriteLine("The given remotepath is not valid. Please re-enter it before continuing.");
                Console.Write("[Escape] Delete this setting\n[Any] Relocate Remotepath\n>");
                if (Console.ReadKey().Key != ConsoleKey.Escape)
                    generateRemotePath();
                else
                    menu.removeMenu(this);
            }
        }

        /// <summary>
        /// Prompts the user to give this setting a name
        /// </summary>
        private void rename()
        {
            Console.Title = "Foldersync setup: Setting Name";
            Console.Clear();
            Console.WriteLine("Name this setting");
            Console.Write(">");
            string input = Console.ReadLine();
            if (input.Length > 2)
            {
                name = input;
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\nPlease enter a longer name");
                rename();
            }
        }

        /// <summary>
        /// Prompts the user to enter a new LocalPath
        /// </summary>
        private void generateLocalPath()
        {
            Console.Title = "Foldersync setup: Localpath";
            Console.WriteLine("Localpath:");
            Console.Write(">");
            string input = Console.ReadLine();

            while (Program.isPathValid(input, 3) == false)
            {
                Console.Clear();
                Console.Write("Localpath:\n>");
                input = Console.ReadLine();
            }
            localpath = input;
            Console.Clear();
        }

        /// <summary>
        /// Prompts the user to enter a new RemotePath
        /// </summary>
        private void generateRemotePath()
        {
            Console.Title = "Foldersync setup: Remotepath";
            Console.WriteLine("Remotepath");
            Console.Write(">");
            string input = Console.ReadLine();

            while (Program.isPathValid(input, 3) == false)
            {
                Console.Clear();
                Console.Write("Remotepath:\n>");
                input = Console.ReadLine();
            }
            remotepath = input;
            Console.Clear();

        }

        /// <summary>
        /// If this is a new object, gather and confirm data from the user
        /// </summary>
        public void firstTimeSetup()
        {
            rename();
            generateLocalPath();
            generateRemotePath();
            Console.Title = "Foldersync setup: Confirmation";
            while (true)
            {
                // check that local and remote path aren't the same
                // go over validation of paths
                Console.WriteLine("Are the details you have entered correct?");
                Console.WriteLine("Name      : " + name);
                Console.WriteLine("Localpath : " + localpath);
                Console.WriteLine("Remotepath: " + remotepath);
                Console.WriteLine("\n[N] Name\n[L] Change Localpath\n[R] Change Remotepath\n[Enter] Confirm");
                ConsoleKey input = Console.ReadKey().Key;
                if (input == ConsoleKey.N)
                {
                    Console.Clear();
                    rename();
                }
                else if (input == ConsoleKey.L)
                {
                    Console.Clear();
                    generateLocalPath();
                }
                else if (input == ConsoleKey.R)
                {
                    Console.Clear();
                    generateRemotePath();
                }
                else if (input == ConsoleKey.Enter)
                {
                    break;
                }
            }
            isFirstTimeSetup = false;
            finished();
        }
        
        private void saveSettings()
        {
            Console.Title = "Foldersync - Saving Settings...";
            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + @"\Foldersync\Settings\" + name + ".setting";

            if (File.Exists(settingsFile))
                File.Delete(settingsFile);

            if (!Directory.Exists(Path.GetDirectoryName(settingsFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(settingsFile));

            using (StreamWriter sw = new StreamWriter(settingsFile))
            {
                sw.WriteLine(name);
                sw.WriteLine(localpath);
                sw.WriteLine(remotepath);
            }
        }

        private void finished()
        {
            saveSettings();
            menu.closeMenu(this);
        }

        // Accessor Methods
        public string getName()
        {
            return name;
        }
        public string getLocalpath()
        {
            return localpath;
        }
        public string getRemotepath()
        {
            return remotepath;
        }
        public bool getFirsttimesetup()
        {
            return isFirstTimeSetup;
        }
    }
}
