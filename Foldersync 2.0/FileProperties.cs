using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync_2._0
{
    class FileProperties
    {
        public string sublocation { get; private set; }
        public bool doCopy;
        public bool doDelete;

        public FileProperties(string sublocation, bool doCopy, bool doDelete)
        {
            this.sublocation = sublocation;
            this.doCopy = doCopy;
            this.doDelete = doDelete;
        }
    }
}
