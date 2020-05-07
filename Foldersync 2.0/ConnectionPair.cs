using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync
{
    class ConnectionPair
    {
        /// <summary>
        /// This class is holding 2 Connections as a pair. The purpose of this is
        /// to have a way of effecitvevly connecting a local location, as well as
        /// any remote location as a pair, to be managed by the ConnectionManager.
        /// </summary>
        public string name { get; private set; }
        public WindowsConnection localEnd { get; private set; }
        public WindowsConnection remoteWinEnd { get; private set; }
        public RemoteConnection remoteEnd { get; private set; }
        public ConnectionPair(string name, WindowsConnection local, WindowsConnection remote)
        {
            this.name = name;
            localEnd = local;
            remoteWinEnd = remote;
        }
        public ConnectionPair(string name, WindowsConnection local, RemoteConnection remote)
        {
            this.name = name;
            localEnd = local;
            remoteEnd = remote;
        }

        public void renameConnection(string updateName)
        {
            if (updateName.Length != 0) // also check if it is just made of spaces
            {
                name = updateName;
            }
        }
        public void changeLocal(WindowsConnection updatedLocal)
        {
            localEnd = updatedLocal;
        }
        public void changeRemote(WindowsConnection updatedRemote)
        {
            remoteWinEnd = updatedRemote;
        }
        public void changeRemote(RemoteConnection updatedRemote)
        {
            remoteEnd = updatedRemote;
        }

        public void save()
        {
            /* Save connection information
             * Use an intelligent system like JSON or XML (still need to learn this...)
             * ENCRYPT THOSE FILES!!!
             */
        }
    }
}
