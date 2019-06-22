using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Foldersync_2._0
{
    class Connection
    {
        /* Tasks:
         * Finish setup for taking in CLI to creating a connection
         *  - Location verification
         *  - Test connection status
         * SSH.NET is installed, set up way to establish SSH Connection
         * Store appropriate information about SSH connection
         * 
         * Set up FTP Connection handling
         * Store appropriate information about FTP connection
         * 
         * Distance user input from this class
         * 
         * Future Plan: support for SSH Keys
         */

        /// <summary>
        /// Connections are holding information of what type of file protocol
        /// is being used, as well as further details about how to access the
        /// deeper file structure of a location. E.g: 
        /// Samba, "C:\Users\Joel\Desktop\get a life"
        /// SSH, Server:Port, Username, Password, root\further\directory
        /// FTP, Server:Port, Username, Password, root\further\directory
        /// </summary>
        public enum ConnectionType
        {
            Samba = 0,
            FTP = 1,
            SSH = 2
        }
        public string location { get; private set; }
        public ConnectionType type { get; private set; }

        public Connection(ConnectionType type, string location)
        {
            this.type = type;
        }

        public static bool verify(ConnectionType type, string path)
        {
            Logger log = new Logger("Path verification");
            if (type == ConnectionType.Samba)
            {
                if (path.Contains(@":\"))
                {
                    if (!Directory.Exists(path))
                    {
                        Console.Write("\nWould you like to create this directory? [y/n]\n>");
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            try
                            {
                                log.writeMessage("Attempting to create " + path);
                                Directory.CreateDirectory(path);
                            }
                            catch (IOException ex)
                            {
                                if (!Directory.Exists(path))
                                {
                                    log.writeMessage("Failed to create " + path);
                                    Console.WriteLine("\n\n[Error]");
                                    Console.WriteLine("Failed to create " + path);
                                }
                                else
                                {
                                    log.writeMessage("Unspecified error during directory creation\n " + ex);
                                    Console.WriteLine("\n\n[Error]");
                                    Console.WriteLine("An unspecified error occured");
                                    Console.WriteLine("When trying to create " + path);
                                }
                                new System.Threading.ManualResetEvent(false).WaitOne(1000);
                                return false;
                            }
                            log.writeMessage("Successfully created " + path);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    try
                    {
                        System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(path);
                        return true;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        log.writeMessage("No write access to " + path);
                        Console.WriteLine("\n\n[Error] Write Access");
                        Console.WriteLine("You have no permissions to write to:\n" + path);
                        new System.Threading.ManualResetEvent(false).WaitOne(1000);
                        return false;
                    }
                }
            }
            else if (type == ConnectionType.FTP)
            {
                // Path verification through FTP connection
                return true;
            }
            else if (type == ConnectionType.SSH)
            {
                // Path verification through FTP connection
                return true;
            }
            return false;
        }
        public static void setNewWinCon()
        {
            Logger log = new Logger("Setting up windows connection");
            bool localVerif = false;
            bool remoteVerif = false;
            string localPath = "";
            string remotePath = "";
            string name = "";

            while (!localVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New Windows Connection ---\n");
                Console.Write("Enter the local location:\n>");
                localPath = Console.ReadLine();
                localVerif = verify(ConnectionType.Samba, localPath);
                Console.Write("\n");
            }

            log.writeMessage("Local path set to " + localPath);
            Connection localConnection = new Connection(ConnectionType.Samba, localPath);

            while (!remoteVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New Windows Connection ---\n");
                Console.Write("Enter the local location:\n>");
                Console.WriteLine(localPath);
                Console.Write("\n");
                Console.Write("Enter the remote location:\n>");
                remotePath = Console.ReadLine();
                remoteVerif = verify(ConnectionType.Samba, remotePath);
            }

            log.writeMessage("Remote path set to " + remotePath);
            Connection remoteConnection = new Connection(ConnectionType.Samba, remotePath);

            Console.Clear();
            Console.WriteLine("--- New Windows Connection between: ---\n");
            Console.WriteLine("[Local] " + localPath + "\n[Remote] " + remotePath);
            Console.Write("\nEnter a name for this connection:\n>");

            // Save this information in some form (yet to learn about JSON, XML or something)
            ConnectionPair cp = new ConnectionPair(Console.ReadLine(), localConnection, remoteConnection);

            Console.WriteLine("Endpoint");
            Console.ReadKey();
        }

        public static void setNewSSHCon()
        {
            bool localVerif = false;
            string localPath = "";
            string remoteIP = "";
            int remotePort = 22;
            string remoteUsername = "";
            string remotePassword = "";
            while (!localVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New SSH Connection ---\n");
                Console.Write("Enter the local location:\n>");
                localPath = Console.ReadLine();
                localVerif = verify(ConnectionType.Samba, localPath);
                Console.Write("\n");
            }
            // Requires proper input verifiction
            // Password to be entered as ***
            // Connection to be tested
            Console.Clear();
            Console.WriteLine("--- New SSH Connection ---\n");
            Console.Write("Enter the remote IP:\n>");
            remoteIP = Console.ReadLine();
            Console.Write("\nEnter the remote Port:\n>");
            Int32.TryParse(Console.ReadLine(), out remotePort);
            Console.Write("\n[Blank if unknown]\nEnter the remote Username:\n>");
            remoteUsername = Console.ReadLine();
            Console.Write("\n[Blank if unknown]\nEnter the remote Password:\n>");
            remotePassword = Console.ReadLine();
        }

        public static void setNewFTPCon()
        {

        }
    }
}
