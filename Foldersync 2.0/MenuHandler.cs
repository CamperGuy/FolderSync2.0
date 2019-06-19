using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Foldersync_2._0
{
    class MenuHandler
    {
        public Menu activeMenu;

        public MenuHandler()
        {
            activeMenu = mainMenu;
        }

        public Menu mainMenu = new Menu("Main Menu");
        Entry mainEnable = new Entry(0, "Enable", ConsoleKey.E);
        Entry mainDisable = new Entry(0, "Disable", ConsoleKey.D);
        Entry mainMngConnection = new Entry(1, "Manage Connections", ConsoleKey.M);
        Entry mainSettings = new Entry(2, "Settings", ConsoleKey.S);
        Entry mainInfo = new Entry(3, "Information", ConsoleKey.I);
        Entry mainQuit = new Entry(4, "Quit", ConsoleKey.Q);

        public Menu manageConnectionsMenu = new Menu("Manage Connections");
        Entry manNewCon = new Entry(0, "Add new connection", ConsoleKey.A);
        Entry manRemoveCon = new Entry(1, "Remove a connection", ConsoleKey.R);
        Entry manChangeState = new Entry(2, "Change sync status of a connection", ConsoleKey.C);
        Entry manBack = new Entry(3, "Back to Main Menu", ConsoleKey.B);

        public Menu settingsMenu = new Menu("Settings");
        Entry settingExample = new Entry(0, "Just an example", ConsoleKey.E);
        Entry settingBack = new Entry(1, "Back", ConsoleKey.B);
        Entry settingQuit = new Entry(2, "Quit", ConsoleKey.Q);

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
        public void checkInput(ConsoleKey input)
        {
            if (activeMenu == mainMenu)
            {
                switch (input)
                {
                    case ConsoleKey.E:
                        // From DISABLED to ENABLED
                        break;
                    case ConsoleKey.D:
                        // From ENABLED to DISABLED
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
                        break;
                    case ConsoleKey.R:
                        // Remove an existing connection
                        break;
                    case ConsoleKey.C:
                        // Change activity status
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
        }

        public void reload()
        {
            if (activeMenu.Equals(mainMenu))
                loadMainMenu();
            else if (activeMenu.Equals(manageConnectionsMenu))
                loadManageConnectionMenu();
            else if (activeMenu.Equals(settingsMenu))
                loadSettingsMenu();
        }

        private void showInfo()
        {
            Console.Clear();
            Console.WriteLine("Okay so here is meant to be some nice text about how to use this application.");
            Console.WriteLine("But I don't really know what to say yet other that this is an empty menu structure");
            Console.WriteLine("\nI love you baby <3");
            Console.Write("\n>");
            Console.ReadKey();
            reload();
        }
    }
}
