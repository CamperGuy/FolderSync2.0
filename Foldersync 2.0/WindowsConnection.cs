using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Renci.SshNet;

namespace Foldersync
{
    class WindowsConnection
    {
        /* Tasks:
         * Finish setup for taking in CLI to creating a connection
         *  - Location verification
         *  - Test connection status
         *  
         * Store appropriate information about SSH connection (encrypt it)
         * 
         * Set up FTP Connection handling
         * Store appropriate information about FTP connection
         * 
         * Distance user input from this class
         * 
         * Store information ENRCYPTED
         */

        /// <summary>
        /// Connections are holding information of what type of file protocol
        /// is being used, as well as further details about how to access the
        /// deeper file structure of a location. E.g: 
        /// Samba, "C:\Users\Joel\Desktop\get a life"
        /// SSH, Server:Port, Username, Password, root\further\directory
        /// SFTP, Server:Port, Username, Password, root\further\directory
        /// </summary>
        public string location { get; private set; }

        public WindowsConnection(string location)
        {
            if(Parser.verifyDir(location))
                this.location = location;
        }
        public bool relocate(string updatedLocation)
        {
            if (Parser.verifyDir(updatedLocation))
            {
                location = updatedLocation;
                return true;
            }
            return false;
        }
    }
}
