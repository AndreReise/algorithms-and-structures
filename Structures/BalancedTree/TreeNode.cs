using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms_and_structures.Structures.BalancedTree
{
    public sealed class TreeNode<T> where T : IComparable
    {
        public TreeNode<T> Left, Right;
        public T Value;
        public int Height;

        public TreeNode(T value)
        {
            Height = 1;
            Value = value;
        }

    }
}
