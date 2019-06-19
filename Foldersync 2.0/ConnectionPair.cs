using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync_2._0
{
    class ConnectionPair
    {
        /// <summary>
        /// This class is holding 2 Connections as a pair. The purpose of this is
        /// to have a way of effecitvevly connecting a local location, as well as
        /// any remote location as a pair, to be managed by the ConnectionManager.
        /// </summary>
        public string name { get; private set; }
        public Connection localEnd { get; private set; }
        public Connection remoteEnd { get; private set; }
        public ConnectionPair(string name, Connection local, Connection remote)
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
        public void changeLocal(Connection updatedLocal)
        {
            localEnd = updatedLocal;
        }
        public void changeRemote(Connection updatedRemote)
        {
            remoteEnd = updatedRemote;
        }
    }
}
