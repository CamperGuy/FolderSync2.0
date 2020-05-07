using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync
{
    class NaryTree<A> 
    {
        NaryTreeNode<A> root;

        public NaryTree()
        {
            this.root = null;
        }

        public NaryTree(NaryTreeNode<A> root)
        {
            this.root = root;
        }

        public int Count()
        {
            if (root == null)
                return 0;
            else
                return root.Count();
        }

        public void PrintTree()
        {
            if (this.root == null)
                Console.WriteLine("[empty]");
            else
                this.root.PrintTree(0);
        }

        public static NaryTree<string> ReadDirectory(string root)
        {
            return new NaryTree<string>(NaryTreeNode<string>.ReadDirectory(new DirectoryInfo(root)));
        }
    }
}
