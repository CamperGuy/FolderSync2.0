using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Foldersync
{
    class MenuHandler
    {
        /// <summary>
        /// This class is responsible for displaying and handling the menu
        /// navigation throughout this program. A new Menu as well as its
        /// entries would be defined, linked together and loaded at the
        /// appropriate user interaction
        /// </summary>
        
        public Menu activeMenu;

        public MenuHandler()
        {
            activeMenu = mainMenu;
        }

        public Menu mainMenu = new Menu("Main Menu");
        MenuEntry mainEnable = new MenuEntry(0, "Enable", ConsoleKey.E);
        MenuEntry mainDisable = new MenuEntry(0, "Disable", ConsoleKey.D);
        MenuEntry mainMngConnection = new MenuEntry(1, "Manage Connections", ConsoleKey.M);
        MenuEntry mainSettings = new MenuEntry(2, "Settings", ConsoleKey.S);
        MenuEntry mainInfo = new MenuEntry(3, "Information", ConsoleKey.I);
        MenuEntry mainQuit = new MenuEntry(4, "Quit", ConsoleKey.Q);

        public Menu manageConnectionsMenu = new Menu("Manage Connections");
        MenuEntry manNewCon = new MenuEntry(0, "Add new connection", ConsoleKey.A);
        MenuEntry manRemoveCon = new MenuEntry(1, "Remove a connection", ConsoleKey.R);
        MenuEntry manChangeState = new MenuEntry(2, "Change sync status of a connection", ConsoleKey.C);
        MenuEntry manBack = new MenuEntry(3, "Back to Main Menu", ConsoleKey.B);

        public Menu settingsMenu = new Menu("Settings");
        MenuEntry settingExample = new MenuEntry(0, "Just an example", ConsoleKey.E);
        MenuEntry settingBack = new MenuEntry(1, "Back", ConsoleKey.B);
        MenuEntry settingQuit = new MenuEntry(2, "Quit", ConsoleKey.Q);

        public Menu newConnectionMenu = new Menu("New Connection Type");
        MenuEntry newWindows = new MenuEntry(0, "New Windows connection", ConsoleKey.W);
        MenuEntry newSSH = new MenuEntry(1, "New SSH connection", ConsoleKey.S);
        MenuEntry newFTP = new MenuEntry(2, "New SFTP connection", ConsoleKey.F);
        MenuEntry newConnectionBack = new MenuEntry(3, "Back", ConsoleKey.B);

        public void loadMainMenu()
        {
            Console.Clear();
            if (FolderSync.RUNNING)
                mainMenu.addItem(mainDisable);
            else
                mainMenu.addItem(mainEnable);
            mainMenu.addItem(mainMngConnection);
            mainMenu.addItem(mainSettings);
            mainMenu.addItem(mainInfo);
            mainMenu.addItem(mainQuit);
            mainMenu.loadMenu();
            activeMenu = mainMenu;
            ConsoleKey input = Console.ReadKey().Key;
            checkInput(input);
        }

        public void loadManageConnectionMenu()
        {
            Console.Clear();
            manageConnectionsMenu.addItem(manNewCon);
            manageConnectionsMenu.addItem(manRemoveCon);
            manageConnectionsMenu.addItem(manChangeState);
            manageConnectionsMenu.addItem(manBack);
            manageConnectionsMenu.addItem(mainQuit);
            manageConnectionsMenu.loadMenu();
            activeMenu = manageConnectionsMenu;
            ConsoleKey input = Console.ReadKey().Key;
            checkInput(input);
        }

        public void loadSettingsMenu()
        {
            Console.Clear();
            settingsMenu.addItem(settingExample);
            settingsMenu.addItem(settingBack);
            settingsMenu.addItem(settingQuit);
            settingsMenu.loadMenu();
            activeMenu = settingsMenu;
            ConsoleKey input = Console.ReadKey().Key;
            checkInput(input);
        }
        public void loadNewConnectionMenu()
        {
            Console.Clear();
            newConnectionMenu.addItem(newWindows);
            newConnectionMenu.addItem(newSSH);
            newConnectionMenu.addItem(newFTP);
            newConnectionMenu.addItem(newConnectionBack);
            newConnectionMenu.loadMenu();
            activeMenu = newConnectionMenu;
            ConsoleKey input = Console.ReadKey().Key;
            checkInput(input);
        }

        public void checkInput(ConsoleKey input)
        {
            if (activeMenu == mainMenu)
            {
                switch (input)
                {
                    case ConsoleKey.E:
                        // From DISABLED to ENABLED
                        Console.WriteLine("\nEndpoint");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D:
                        // From ENABLED to DISABLED
                        Console.WriteLine("\nEndpoint");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.M:
                        loadManageConnectionMenu();
                        break;
                    case ConsoleKey.S:
                        loadSettingsMenu();
                        break;
                    case ConsoleKey.I:
                        showInfo();
                        break;
                    case ConsoleKey.Q:
                        // Quit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Write("\n\n\nInvalid input, please try again ");
                        Thread.Sleep(1000);
                        Console.Clear();
                        reload();
                        break;
                }
            }
            else if (activeMenu == manageConnectionsMenu)
            {
                switch (input)
                {
                    case ConsoleKey.A:
                        // Add a connection
                        loadNewConnectionMenu();
                        break;
                    case ConsoleKey.R:
                        // Remove an existing connection
                        Console.WriteLine("Load in existing connections from a file that does not yet exist");
                        Console.WriteLine("In other words;\nEndpoint");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.C:
                        // Change activity status
                        Console.WriteLine("Load in existing connections from a file that does not yet exist");
                        Console.WriteLine("In other words;\nEndpoint");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.B:
                        // Back
                        loadMainMenu();
                        break;
                    case ConsoleKey.Q:
                        // Quit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Write("\n\n\nInvalid input, please try again ");
                        Console.WriteLine(input.ToString());
                        Thread.Sleep(1000);
                        Console.Clear();
                        reload();
                        break;
                }
            }
            else if (activeMenu == settingsMenu)
            {
                switch (input)
                {
                    case ConsoleKey.B:
                        loadMainMenu();
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Write("\n\n\nInvalid input, please try again ");
                        Console.WriteLine(input.ToString());
                        Thread.Sleep(1000);
                        Console.Clear();
                        reload();
                        break;
                }
            }
            else if (activeMenu == newConnectionMenu)
            {
                switch (input)
                {
                    case (ConsoleKey.W):
                        ConnectionManager.cliNewWindowsConnection();
                        break;
                    case (ConsoleKey.S):
                        ConnectionManager.cliNewSSHConnection();
                        break;
                    case (ConsoleKey.F):
                        ConnectionManager.cliNewSFTPConnection();
                        break;
                    case (ConsoleKey.B):
                        loadManageConnectionMenu();
                        break;
                    default:
                        Console.Write("\n\n\nInvalid input, please try again ");
                        Console.WriteLine(input.ToString());
                        Thread.Sleep(1000);
                        Console.Clear();
                        reload();
                        break;

                }
            }
        }

        public void reload()
        {
            if (activeMenu.Equals(mainMenu))
                loadMainMenu();
            else if (activeMenu.Equals(manageConnectionsMenu))
                loadManageConnectionMenu();
            else if (activeMenu.Equals(settingsMenu))
                loadSettingsMenu();
            else if (activeMenu.Equals(newConnectionMenu))
                loadNewConnectionMenu();
        }

        private void showInfo()
        {
            Console.Clear();
            Console.WriteLine("--- Information ---");
            Console.WriteLine("\nThis program will sync a remote location to a specified folder on your local machine.");
            Console.WriteLine("\nFor more detailed information visit:\nhttps://github.com/CamperGuy/Foldersync2.0/README.md");
            Console.Write("\n>");
            Console.ReadKey();
            reload();
        }
    }
}
