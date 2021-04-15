using System;
using System.Collections.Generic;
using algorithms_and_structures.Structures.BalancedTree;

namespace algorithms_and_structures.Structures
{
    interface ITree<T> where  T : IComparable
    {
        bool Search(T value);
        List<T> PrintSorted();

        /// <summary>
        /// Finds the sum of right key`s of specified node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        T RightKeySum(TreeNode<T> node);

        /// <summary>
        /// Counts the number of node`s sons in tree
        /// </summary>
        int CountNodeDescendants(TreeNode<T> node);

        public List<T> GetPreOrderList();
    }
}
