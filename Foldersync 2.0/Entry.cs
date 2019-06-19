using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync_2._0
{
    public class Entry 
    {
        public Entry(int index, string content, ConsoleKey key)
        {
            this.index = index;
            this.content = content;
            this.key = key;
        }
        public int index { get; private set; }
        public string content { get; private set; }
        public ConsoleKey key { get; private set; }
    }
}
