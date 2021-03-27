using System;

namespace algorithms_and_structures.Structures.BalancedTree
{
    public interface IBalancedTree<T> where T : IComparable
    {
        public bool IsBalanced();
        public T FatherNodeKey(T value);
        public  BalancedTree<T> CopyBalancedTree();
        public void InsertBalancedTree(BalancedTree<T> tree);

    }
}