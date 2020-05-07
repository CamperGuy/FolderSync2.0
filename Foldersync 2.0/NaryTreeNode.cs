using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Foldersync
{
    class NaryTreeNode<A>
    {
        A value;
        A extra;
        List<NaryTreeNode<A>> children;

        public NaryTreeNode(A value)
        {
            this.value = value;
            this.children = new List<NaryTreeNode<A>>();
        }

        public NaryTreeNode(A value, A extra)
        {
            this.value = value;
            this.extra = extra;
            this.children = new List<NaryTreeNode<A>>();
        }

        public void AddChild(NaryTreeNode<A> child)
        {
            this.children.Add(child);
        }

        public int Count()
        {
            int total = 1;
            foreach (NaryTreeNode<A> child in children)
                total += child.Count();
            return total;
        }

        public int Depth()
        {
            int maxDepth = 0;
            foreach (NaryTreeNode<A> child in children)
            {
                int childDepth = child.Depth();
                if (childDepth > maxDepth)
                    maxDepth = childDepth;
            }
            return 1 + maxDepth;
        }

        public void PrintTree(int spaces)
        {
            string space = " ";
            for (int i = 0; i < spaces; i++)
                space += " ";
            if (extra == null)
                Console.WriteLine(space + " * " + value.ToString());
            else
                Console.WriteLine(space + " * " + value.ToString() + " " + extra.ToString());

            foreach (NaryTreeNode<A> child in children)
                child.PrintTree(spaces + 3);
        }


        public static NaryTreeNode<string> ReadDirectory(DirectoryInfo dir)
        {
            NaryTreeNode<string> node = new NaryTreeNode<string>(dir.Name, dir.LastWriteTimeUtc.ToString());
            foreach(string name in Directory.GetDirectories(dir.FullName))
            {
                DirectoryInfo sub = new DirectoryInfo(name);
                node.AddChild(ReadDirectory(sub));
            }
            foreach(string name in Directory.GetFiles(dir.FullName))
            {
                FileInfo sub = new FileInfo(name);
                node.AddChild(new NaryTreeNode<string>(sub.Name, sub.LastWriteTimeUtc.ToString()));
            }
            return node;
        }
    }
}
