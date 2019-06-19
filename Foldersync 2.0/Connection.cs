using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync_2._0
{
    class Connection
    {
        public enum ConnectionType
        {
            Samba = 0,
            SFTP = 1,
            SSH = 2
        }
        public string localLocation { get; private set; }
        public string remoteLocation { get; private set; }
        public ConnectionType type { get; private set; }

        public Connection(ConnectionType type, string localLocation, string remoteLocation)
        {
            this.type = type;
        }
    }
}
