using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using algorithms_and_structures.Structures.BalancedTree;
using algorithms_and_structures.Utils;

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

            tree.PrintPreOrder();
            Console.WriteLine();

            tree.Root = tree.DeleteNode(tree.Root, 10);
            tree.PrintPreOrder();

            var newTree = new BalancedTree<int>();
            newTree.Root = newTree.Insert(newTree.Root, 77);
            newTree.Root = newTree.Insert(newTree.Root, 7);
            newTree.Root = newTree.Insert(newTree.Root, 8);

            //tree.InsertBalancedTree(newTree);
            Console.WriteLine("New tree : ");
            tree.PrintSorted(Printer.SortType.Ascending);

            var fn = tree.Equals(newTree);

        }
    }
}
