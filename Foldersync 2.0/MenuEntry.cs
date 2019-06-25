using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync_2._0
{
    public class MenuEntry 
    {
        public MenuEntry(int index, string content, ConsoleKey key)
        {
            this.index = index;
            this.content = content;
            this.key = key;
        }
        public int index { get; private set; }
        public string content { get; private set; }
        public ConsoleKey key { get; private set; }
        public void relocate(int updatedIndex)
        {
            index = updatedIndex;
        }
        public void rename(string updatedContent)
        {
            if (updatedContent.Length != 0)
                content = updatedContent;
        }
    }
}
