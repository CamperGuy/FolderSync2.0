using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;

namespace Foldersync
{
    class Program
    {
        #region Console Config Stuff
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        #endregion

        private static MainMenu menu = new MainMenu();
        static void Main(string[] args)
        {
            #region Lock Console Size
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
            #endregion
            // Change Console Encoding to support printing arrows (↑)
            Console.OutputEncoding = Encoding.Unicode;

            // Make the console smaller and remove the vertical scroll bar
            Console.SetWindowSize(80, 20);
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            // Set the console Title
            Console.Title = "Foldersync";

            menu.loadMenus();
        }

        /// <summary>
        /// Return whether a location is valid or not
        /// </summary>
        /// <param name="path">The path that will be looked at</param>
        /// <param name="mode">0 for not attempting to make Directory and not giving console output. 1 for giving console output only. 2 for attempting to create a directory only. 3 for everything enabled</param>
        /// <returns></returns>
        public bool isPathValid(string path, int mode)
        {
            if (path.Contains(@"\"))
            {
                if (Directory.Exists(path))
                {
                    try
                    {
                        Directory.GetAccessControl(path);
                        if (mode == 1 || mode == 3)
                            Console.WriteLine(path + " is valid");
                        return true;
                    }
                    catch
                    {
                        if (mode == 1 || mode == 3)
                        {
                            Console.WriteLine(path + " denies access.");
                            new System.Threading.ManualResetEvent(false).WaitOne(500);
                        }
                        return false;
                    }
                }
                else
                {
                    if (mode == 2 || mode == 3)
                    {
                        try
                        {
                            Directory.CreateDirectory(path);
                            try
                            {
                                Directory.GetAccessControl(path);
                                Console.WriteLine("Got access");
                                if (mode == 3)
                                    Console.WriteLine(path + " has been created and is valid");

                                Console.ReadKey();
                                return true;
                            }
                            catch
                            { 
                                if (mode == 3)
                                {
                                    Console.WriteLine(path + " has been created but it denies access.");
                                    new System.Threading.ManualResetEvent(false).WaitOne(500);
                                }
                                return false;
                            }
                        }
                        catch
                        {
                            if (mode == 3)
                            {
                                Console.WriteLine(path + " has been attempted to be created. Failed");
                                new System.Threading.ManualResetEvent(false).WaitOne(500);
                            }
                            return false;
                        }
                    }
                    else
                        return false;
                }
            }
            else
            {
                if (mode == 1 || mode == 3)
                Console.WriteLine("The path you entered is not of a valid format\nMust point to folder with " + @"\");
                new System.Threading.ManualResetEvent(false).WaitOne(500);
                return false;
            }
        }
    }
}
