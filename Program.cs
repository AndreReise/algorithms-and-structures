using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using algorithms_and_structures.Structures.BalancedTree;


namespace algorithms_and_structures
{
    class Program
    {
        static void Main(string[] args)
        {
            BalancedTree<int> tree = new BalancedTree<int>();

            List<int> insertList = new List<int> {9, 5, 10, 0, 6, 11, -1, 1, 2};

            foreach (var item in insertList)
            {
                tree.Root = tree.Insert(tree.Root, item);
            }

            tree.PrintSorted();

            tree.Root = tree.DeleteNode(tree.Root, -1);

            tree.PrintSorted();

        }
    }
}
