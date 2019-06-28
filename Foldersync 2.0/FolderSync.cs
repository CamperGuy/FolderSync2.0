using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync_2._0
{
    abstract class FolderSync
    {
        public static bool RUNNING;
        private static MenuHandler menuHandler;
        static void Main(string[] args)
        {
            consoleSetup();
            // Parser.writeXML();
            menuHandler = new MenuHandler();
            menuHandler.loadMainMenu();
        }

        private static void consoleSetup()
        {
            if (RUNNING)
                Console.Title = "Foldersync - Enabled";
            else
                Console.Title = "Foldersync - Disabled";

            Console.SetWindowSize(Console.LargestWindowWidth / 3, Console.LargestWindowHeight / 3);
            Console.SetBufferSize(Console.LargestWindowWidth / 3, Console.LargestWindowHeight / 3);
        }

        public static void CONSOLE_RESET()
        {
            Console.Clear();
            consoleSetup();
        }
    }
}
