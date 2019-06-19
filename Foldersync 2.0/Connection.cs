using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (type == ConnectionType.Samba)
            {
                // Path verification through windows connection
                return true;
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
            bool localVerif = false;
            bool remoteVerif = false;
            string localPath = "";
            string remotePath = "";
            while (!localVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New Windows Connection ---\n");
                Console.Write("Enter the local location:\n>");
                localPath = Console.ReadLine();
                localVerif = verify(ConnectionType.Samba, localPath);
                Console.Write("\n");
            }
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
            Console.WriteLine("Debug point");
            Console.ReadKey();
        }

        public static void setNewSSHCon()
        {

        }

        public static void setNewFTPCon()
        {

        }
    }
}
