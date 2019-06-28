using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;
using Renci.SshNet.Common;

namespace Foldersync_2._0
{
    class RemoteConnection
    {
        public string host { get; private set; }
        public int port { get; private set; }
        public string username { get; private set; }

        private string password;
        private PrivateKeyFile keyFile;
        private ConnectionManager.ConnectionType connectionType;
        public SshClient sshClient { get; private set; }
        public SftpClient sftpClient { get; private set; }
        public WindowsConnection windowsLocal { get; private set; }
        public RemoteConnection remoteLocal { get; private set; }
        public string entryPoint = "";

        public void linkLocal(WindowsConnection winCon)
        {
            windowsLocal = winCon;
        }
        public void linkLocal(RemoteConnection remCon)
        {
            remoteLocal = remCon;
        }
        public RemoteConnection(ConnectionManager.ConnectionType connectionType, string host, int port, string username, PrivateKeyFile keyFile)
        {
            sshClient = null;
            this.connectionType = connectionType;
            this.host = host;
            this.port = port;
            this.username = username;
            this.keyFile = keyFile;
            PrivateKeyConnectionInfo keyInfo = new PrivateKeyConnectionInfo(host, port, username, keyFile);

            if (connectionType == ConnectionManager.ConnectionType.SSH)
                sshClient = new SshClient(keyInfo);

            else if (connectionType == ConnectionManager.ConnectionType.SFTP)
                sftpClient = new SftpClient(keyInfo);
        }
        public RemoteConnection(ConnectionManager.ConnectionType connectionType, string host, int port, string username, string password)
        {
            sshClient = null;
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
            PasswordConnectionInfo passwordInfo = new PasswordConnectionInfo(host, port, username, password);

            if (connectionType == ConnectionManager.ConnectionType.SSH)
                sshClient = new SshClient(passwordInfo);
            else if (connectionType == ConnectionManager.ConnectionType.SFTP)
                sftpClient = new SftpClient(passwordInfo);
        }

        
    }
}
