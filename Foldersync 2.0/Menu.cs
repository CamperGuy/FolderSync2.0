using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Foldersync_2._0
{
    class Menu
    {
        private SortedDictionary<int, MenuEntry> entries = new SortedDictionary<int, MenuEntry>();
        private List<ConsoleKey> keys = new List<ConsoleKey>();
        public string name { get; private set; }
        
        public Menu(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// int addItem(Entry) validates the Entry and, if possible, adds them to the current Menu
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public int addItem(MenuEntry entry)
        {
            if (!entries.ContainsKey(entry.index) && !keys.Contains(entry.key))
            {
                entries.Add(entry.index, entry);
                keys.Add(entry.key);
            }
            return 0;
        }

        /// <summary>
        /// int removeItem(Entry) removes a certain entry form the Menu
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public int removeItem(MenuEntry entry)
        {
            if (entries.ContainsKey(entry.index) && entries.ContainsValue(entry) && keys.Contains(entry.key))
            {
                entries.Remove(entry.index);
                keys.Remove(entry.key);
            }
            return 0;
        }
        
        /// <summary>
        /// int loadMenu() displays all menu 'entries' into the console
        /// </summary>
        public int loadMenu()
        {
            if (entries.Count() == 0)
            { 
                return 1;
            }
            int run = 0;
            foreach (KeyValuePair<int, MenuEntry> entry in entries)
            {
                if (run == 0)
                    Console.WriteLine("--- " + name + " ---\n");
                Console.Write("[" + entry.Value.key.ToString() + "] " + entry.Value.content + "\n");
                run++;
            }
            Console.Write("\n>");
            return 0;
        }
    }
}
