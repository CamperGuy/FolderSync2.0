using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Foldersync_2._0
{
    class Parser
    {
        public string path1 { get; private set; }
        public string path2 { get; private set; }

        private List<FileProperties> path1Files = new List<FileProperties>();
        private List<FileProperties> path2Files = new List<FileProperties>();
        private List<string> path1Directories = new List<string>();
        private List<string> path2Directories = new List<string>();

        public Parser(string sourcePath, string destinationPath)
        {
            path1 = sourcePath;
            path2 = destinationPath;
        }
        private static bool checkIfEmpty(string source, string target)
        {
            return true;
        }

        public void mirrorFileStructure()
        {
            Parser.checkIfEmpty(path1, path2);
            loadFileStructure();
            eraseDifferingStructure();
            copyMissingDirectories();
            copyMissingFiles();
            updateExistingFiles();
        }
        public static void mirrorFileStructure(string source, string target)
        {
            Parser parser = new Parser(source, target);
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
    }
}
