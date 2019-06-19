using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Foldersync_2._0
{
    class Logger
    {
        /* Tasks:
         * - Implement non-static complete function to print a message stating
         * that the current task (as initialised with) has been completed
         */

        /// <summary>
        /// This class' purpose is to have an easily available way of
        /// printing into a log file. For each major task i.e. creating
        /// a new Connection, a new Logger object would be created. The
        /// constructor would handle appropriate formatting. The same
        /// object could then be used via loggerObject.writeMessage("Message")
        /// to append to the current task.
        /// </summary>
        private static string path = Path.GetTempPath() + @"\foldersync_log.txt";
        public Logger(string task)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(task + " started at: " + DateTime.Now.ToString("HH:mm:ss"));
            writer.Close();
        }
        public static void write(string message)
        {
            StreamWriter writer = new StreamWriter(path);
            string time = DateTime.Now.ToString("HH:mm:ss");
            writer.WriteLine("[" + time + "]" + message);
            writer.Close();
        }
        public void writeMessage(string message)
        {
            StreamWriter writer = new StreamWriter(path);
            string time = DateTime.Now.ToString("HH:mm:ss");
            writer.WriteLine("[" + time + "]" + message);
            writer.Close();
        }
        public static void open()
        {
            System.Diagnostics.Process.Start(path);
        }
        public static void deleteCurrentLog()
        {
            File.Delete(path);
        }
    }
}
