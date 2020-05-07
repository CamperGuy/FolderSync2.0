using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;
using Renci.SshNet.Common;

namespace Foldersync
{
    class ConnectionManager
    {
        public enum ConnectionType
        {
            Samba = 0,
            SFTP = 1,
            SSH = 2
        }
        public static void cliNewWindowsConnection()
        {
            Logger log = new Logger("Setting up windows connection");
            WindowsConnection localConnection;
            WindowsConnection remoteConnection;

            string localPath = "";
            string remotePath = "";

            bool localVerif = false;
            while (!localVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New Windows Connection ---\n");
                Console.Write("Enter the local location:\n>");
                localPath = Console.ReadLine();
                localVerif = Parser.verifyDir(localPath);
                Console.Write("\n");
            }

            log.writeMessage("Local path set to " + localPath);
            localConnection = new WindowsConnection(localPath);

            bool remoteVerif = false;
            while (!remoteVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New Windows Connection ---\n");
                Console.Write("Enter the local location:\n>");
                Console.WriteLine(localPath);
                Console.Write("\n");
                Console.Write("Enter the remote location:\n>");
                remotePath = Console.ReadLine();
                remoteVerif = Parser.verifyDir(remotePath);
            }

            log.writeMessage("Remote path set to " + remotePath);
            remoteConnection = new WindowsConnection(remotePath);

            Console.Clear();
            Console.WriteLine("--- New Windows Connection between: ---\n");
            Console.WriteLine("[Local] " + localPath + "\n[Remote] " + remotePath);
            Console.Write("\nEnter a name for this connection:\n>");

            // Save this information in some form (yet to learn about JSON, XML or something)
            ConnectionPair cp = new ConnectionPair(Console.ReadLine(), localConnection, remoteConnection);
            cp.save();

            Console.WriteLine("Endpoint");
            Console.ReadKey();
        }
        public static void cliNewSSHConnection()
        {
            string localPath = "";
            string remoteHost = "";
            int remotePort = 22;
            string username = "";
            string password = "";
            PrivateKeyFile keyFile = null;

            WindowsConnection localEnd;
            RemoteConnection remote = null;

            bool localVerif = false;
            while (!localVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New SSH Connection ---\n");
                Console.Write("Enter the local location:\n>");
                localPath = Console.ReadLine();
                localVerif = Parser.verifyDir(localPath);
                Console.Write("\n");
            }
            localEnd = new WindowsConnection(localPath);

            // Requires proper input verifiction (namely port)
            Console.Clear();
            Console.WriteLine("--- New SSH Connection ---\n");
            Console.Write("Enter the host:\n>");
            remoteHost = Console.ReadLine();
            Console.Write("\nEnter the remote Port:\n>");
            Int32.TryParse(Console.ReadLine(), out remotePort);
            Console.Write("\nEnter the remote Username:\n>");
            username = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("--- SSH Setup to " + remoteHost + " ---");
            Console.Write("\nDo you wish to use a Password or Key?\n\n[P] Password\n[K] Key\n>");
            ConsoleKey input = Console.ReadKey().Key;

            if (input == ConsoleKey.P)
            {
                Console.Write("\n\nEnter the remote Password:\n>");

                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
            }
            else if (input == ConsoleKey.K)
            {
                string keyPath = "";
                bool verified = false;
                while (!verified)
                {
                    Console.Clear();
                    Console.WriteLine("--- Specify SSH Key ---");
                    Console.Write("\nSpecify the path of the key:\n>");
                    keyPath = Console.ReadLine();
                    verified = Parser.verifyFile(keyPath);
                    try
                    {
                        keyFile = new PrivateKeyFile(keyPath);
                    }
                    catch (Renci.SshNet.Common.SshException)
                    {
                        Console.WriteLine("Key file is invalid");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("--- Name connection to: " + remoteHost + "---");
            Console.Write("\n\nEnter a name:\n>");
            if (keyFile == null)
                remote = new RemoteConnection(ConnectionType.SSH, remoteHost, remotePort, username, password);
            else
                remote = new RemoteConnection(ConnectionType.SSH, remoteHost, remotePort, username, keyFile);

            ConnectionPair cp = new ConnectionPair(Console.ReadLine(), localEnd, remote);
            cp.save();

            SSHSetup(remote);
            Console.WriteLine("Endpoint");
            Console.ReadKey();
        }
        public static void cliNewSFTPConnection()
        {
            string localPath = "";
            string remoteHost = "";
            int remotePort = 22;
            string username = "";
            string password = "";
            PrivateKeyFile keyFile = null;

            WindowsConnection localEnd;
            RemoteConnection remoteEnd = null;

            bool localVerif = false;
            while (!localVerif)
            {
                Console.Clear();
                Console.WriteLine("--- New SFTP Connection ---\n");
                Console.Write("Enter the local location:\n>");
                localPath = Console.ReadLine();
                localVerif = Parser.verifyDir(localPath);
                Console.Write("\n");
            }
            localEnd = new WindowsConnection(localPath);

            // Requires proper input verifiction (namely port)
            Console.Clear();
            Console.WriteLine("--- New SFTP Connection ---\n");
            Console.Write("Enter the host:\n>");
            remoteHost = Console.ReadLine();
            Console.Write("\nEnter the remote Port:\n>");
            Int32.TryParse(Console.ReadLine(), out remotePort);
            Console.Write("\nEnter the remote Username:\n>");
            username = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("--- SFTP Setup to " + remoteHost + " ---");
            Console.Write("\nDo you wish to use a Password or Key?\n\n[P] Password\n[K] Key\n>");
            ConsoleKey input = Console.ReadKey().Key;

            if (input == ConsoleKey.P)
            {
                Console.Write("\n\nEnter the remote Password:\n>");

                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
            }
            else if (input == ConsoleKey.K)
            {
                string keyPath = "";
                bool verified = false;
                while (!verified)
                {
                    Console.Clear();
                    Console.WriteLine("--- Specify SFTP Key ---");
                    Console.Write("\nSpecify the path of the key:\n>");
                    keyPath = Console.ReadLine();
                    verified = Parser.verifyFile(keyPath);
                    try
                    {
                        keyFile = new PrivateKeyFile(keyPath);
                    }
                    catch (Renci.SshNet.Common.SshException)
                    {
                        Console.WriteLine("Key file is invalid");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            remoteEnd = new RemoteConnection(ConnectionType.SFTP, remoteHost, remotePort, username, keyFile);

            Console.Clear();
            Console.WriteLine("--- Name connection to: " + remoteHost + "---");
            Console.Write("\n\nEnter a name:\n>");
            ConnectionPair cp = new ConnectionPair(Console.ReadLine(), localEnd, remoteEnd);
            cp.save();

            Console.WriteLine("Endpoint");
            Console.ReadKey();
        }

        public static void SSHSetup(RemoteConnection remote)
        {
            Console.Clear();
            Console.WriteLine("--- Testing Connection to: " + remote.host);
            Console.WriteLine("\n- Resolving Hostname...");
            // Put all of this in tries etc
            // Holy shit this function needs sorting out. But I'm just learning, okay? Tanks.
            remote.sshClient.Connect();
            Console.WriteLine("- Confirming Fingerprints...");
            // Accept the fingerprint
            remote.sshClient.HostKeyReceived += delegate (object sender, HostKeyEventArgs e) { e.CanTrust = true; };

            string foldersyncStatus = "";
            if (FolderSync.RUNNING == true)
                foldersyncStatus = "Enabled";
            else
                foldersyncStatus = "Disabled";
            Console.Title = "Foldersync - SSH to " + remote.host + " - " + foldersyncStatus;

            Console.WriteLine("\n[empty for root]\nPlease set the folder to sync");
            Console.Write("\n>");
            remote.entryPoint = Console.ReadLine();

            // This is where the logic will step in
            // Run this through the parser and shit
            var cmd = remote.sshClient.CreateCommand("ls " + remote.entryPoint);
            var result = cmd.Execute();

            Console.Write(result);
            // Needs a proper fingerprint check but idfk how to
            // Where/how does the local fingerprint get generated?
            /*
             *  public static byte[] ConvertFingerprintToByteArray(String fingerprint)
                {
                    return fingerprint.Split(':').Select(s => Convert.ToByte(s, 16)).ToArray();
                }
                client.HostKeyReceived += delegate (object sender, HostKeyEventArgs e)
                {
                    if (e.FingerPrint.SequenceEqual(ConvertFingerprintToByteArray("1d:c1:5a:71:c4:8e:a3:ff:01:0a:3b:46:17:6f:e1:52")))
                        e.CanTrust = true;
                    else
                        e.CanTrust = false;
                };

                To make a shell:
                var shell = client.CreateShell(input, Console.OpenStandardOutput(), new MemoryStream());
                shell.Start();
                StreamWriter.WriteLine("scp");
                input.Position = 0;
                Console.ReadKey();
                client.Disconnect();
                client.Dispose();
             */
        }
    }
}
