using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtils
{
    internal class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public Node()
        {
        }
        public Node(T item)
        {
            Item = item;
        }
    }
}
