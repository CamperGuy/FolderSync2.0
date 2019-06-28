using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Foldersync_2._0
{
    class Parser
    {
        /* Tasks:
         * 
         * Update internal functions to return a boolean value
         * This would allow to only write a corresponding log message when
         * the function has made changes to any file structures to prevent
         * writing logs when nothing has actually been done
         * 
         * Add Logger to corresponding steps (currently in Object-Method mirrorFileStructure)
         * 
         * checkIfEmpty actually needs to do something
         * 
         * Standardise to account for different connection types
         */

         /// <summary>
         /// The Parser is responsible for any file handling. Mainly mirroring
         /// file structures of two location
         /// </summary>
        public string path1 { get; private set; }
        public string path2 { get; private set; }

        private Logger logger = new Logger("Parser");
        private static string xmlpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Foldersync.xml");

        public WindowsConnection connectionType { get; private set; }

        private List<FileProperties> path1Files = new List<FileProperties>();
        private List<FileProperties> path2Files = new List<FileProperties>();
        private List<string> path1Directories = new List<string>();
        private List<string> path2Directories = new List<string>();

        public Parser(WindowsConnection type, string sourcePath, string destinationPath)
        {
            path1 = sourcePath;
            path2 = destinationPath;
            connectionType = type;
        }

        private static bool checkIfEmpty(string source, string target)
        {
            return true;
        }

        public void mirrorFileStructure()
        {
            Parser.checkIfEmpty(path1, path2);
            loadFileStructure();
            logger.writeMessage("-- Loaded file structure");
            eraseDifferingStructure();
            logger.writeMessage("--- Deleted mismatched files");
            copyMissingDirectories();
            logger.writeMessage("--- Copied directory structure");
            copyMissingFiles();
            logger.writeMessage("--- Copied missing files");
            updateExistingFiles();
            logger.writeMessage("--- Updated existing files");
        }
        public static void mirrorFileStructure(WindowsConnection type, string source, string target)
        {
            Parser parser = new Parser(type, source, target);
            parser.loadFileStructure();
            parser.eraseDifferingStructure();
            parser.copyMissingDirectories();
            parser.copyMissingFiles();
            parser.updateExistingFiles();
        }
        public void loadFileStructure()
        {   
            // Get all the directories in the sourcepath
            foreach(string fullpath in Directory.GetDirectories(path1, "*", SearchOption.AllDirectories))
            {
                string subpath = fullpath.Remove(0, path1.Count());
                path1Directories.Add(subpath);
            }

            // Get all the directories in the targetpath
            foreach(string fullpath in Directory.GetDirectories(path2, "*", SearchOption.AllDirectories))
            {
                string subpath = fullpath.Remove(0, path2.Count());
                path2Directories.Add(subpath);
            }

            // Get all the files in the sourcepath
            foreach(string fullpath in Directory.GetFiles(path1, "*", SearchOption.AllDirectories))
            {
                string subpath = fullpath.Remove(0, path1.Count());
                FileProperties file = new FileProperties(subpath, true, false);
                path1Files.Add(file);
            }

            // Get all the files in the targetpath
            foreach(string fullpath in Directory.GetFiles(path2, "*", SearchOption.AllDirectories))
            {
                string subpath = fullpath.Remove(0, path2.Count());
                FileProperties file = new FileProperties(subpath, false, false);
            }
        }
        public void eraseDifferingStructure()
        {
            Parser.checkIfEmpty(path1, path2);

            // Remove mismatched items from lists
            List<string> toDeleteDirectories = path2Directories;
            List<FileProperties> toDeleteFiles = path2Files;
            toDeleteDirectories.RemoveAll(item => !path1Directories.Contains(item));
            toDeleteFiles.RemoveAll(item => !path1Files.Contains(item));

            // Iterate over lists and delete their contained items
            foreach (FileProperties file in toDeleteFiles)
                File.Delete(path2 + file.sublocation);

            foreach (string dir in toDeleteDirectories)
                Directory.Delete(dir);
        }
        public void copyMissingDirectories()
        {
            for (int i = 0; i < path1Directories.Count(); i++)
            {
                if (!Directory.Exists(path2 + path1Directories[i]))
                    Directory.CreateDirectory(path2 + path1Directories[i]);
            }
        }
        public void copyMissingFiles()
        {
            for (int i = 0; i < path1Files.Count(); i++)
            {
                if (!File.Exists(path2 + path1Files[i]))
                    File.Copy(path2 + path1Files[i], path2 + path1Files[i]);
            }
        }
        public static void copy(WindowsConnection type, string sourcepath, string targetpath)
        {
            Parser parser = new Parser(type, sourcepath, targetpath);
            parser.loadFileStructure();
            parser.copyMissingDirectories();
            parser.copyMissingFiles();
            parser.updateExistingFiles();
        }
        public void updateExistingFiles()
        {
            for (int i = 0; i < path1Files.Count(); i++)
            {
                if (File.Exists(path2 + path1Files[i]))
                {
                    DateTime timeSource = File.GetLastWriteTime(path1 + path1Files[i]);
                    DateTime timeTarget = File.GetLastWriteTime(path2 + path1Files[i]);
                    if (timeSource.Ticks > timeTarget.Ticks)
                        File.Copy(path1 + path1Files[i], path2 + path1Files[i], true);
                }
            }
        }

        public static bool verifyDir(string path)
        {
            Logger log = new Logger("Path verification");
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
            return false;
        }
        public static bool verifyFile(string path)
        {
            if (path.Contains(@":\"))
            {
                if (File.Exists(path))
                    return true;
            }
            return false;
        }
        public static void writeXML(ConnectionPair pair)
        {
            if (!File.Exists(xmlpath))
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                xmlSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create(xmlpath, xmlSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Connections");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Settings");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndDocument();
                    xmlWriter.Close();
                }
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNodeList connectionNodes = xmlDoc.SelectNodes("//Connections/");
                foreach (XmlNode connectionNode in connectionNodes)
                {
                    int age = int.Parse(connectionNode.Attributes["age"].Value);
                    connectionNode.Attributes["age"].Value = (age + 1).ToString();
                }
                xmlDoc.Save(xmlpath);
            }
        }
    }
}
