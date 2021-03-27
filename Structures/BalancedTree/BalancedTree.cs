using System;
using System.Linq;


namespace algorithms_and_structures.Structures.BalancedTree
{
    

    public partial class BalancedTree<T> : ITree<T> , IBalancedTree<T> where T : IComparable
    {
        private bool _consistDuplicates;
        

        public int Size { get; private set; }

        public TreeNode<T> Root;

        public BalancedTree()
        {
            Size = 0;
            _consistDuplicates = false;
            Root = null;
        }

        public bool Search(T value)
        {
            var node = this.Root;

            while (node != null)
            {
                if (node.Value.Equals(value))
                    return true;

                node = node.Value.CompareTo(value) == 1 ? node.Left : node.Right;
            }


            return false;
        }

        public bool IsBalanced()
        {
            return Math.Abs(GetBalance(this.Root)) <=1;
        }

        public  bool Equals(BalancedTree<T> tree)
        {
            if (tree == null) return false;

            if (this == tree) return true;

            var thisListShape = this.GetPreOrderList();
            var otherListShape = tree.GetPreOrderList();

            return thisListShape.SequenceEqual(otherListShape);
        }

        public T FatherNodeKey(T value)
        {
            var node = this.Root;

            while (node != null)
            {
                if ((node.Left != null && node.Left.Value.Equals(value)) ||
                    node.Right != null && node.Right.Value.Equals(value))
                {
                    return node.Value;
                }


                if (node.Value.CompareTo(value) == 1)
                {
                    node = node.Left;
                }

                if (node.Value.CompareTo(value) == -1)
                {
                    node = node.Right;
                }
            }

            return default(T);
        }

        public BalancedTree<T> CopyBalancedTree()
        {
            BalancedTree<T> newTree = new BalancedTree<T>();
            newTree.Root = CopyNode(this.Root);

            return newTree;
        }

        private TreeNode<T> CopyNode(TreeNode<T> node)
        {
            if (node == null) return null;

            TreeNode<T> newNode = new TreeNode<T>(node.Value);
            newNode.Right = CopyNode(node.Right);
            newNode.Left = CopyNode(node.Left);

            return newNode;
        }

       

        public T RightKeySum(TreeNode<T> node)
        {
            dynamic sum = node.Value;
            node = node.Right;

            while (node!=null)
            {
                sum += node.Value;
                node = node.Right;
            }

            return sum;
        }

        public int CountNodeDescendants(TreeNode<T> node)
        {
            int count = 0;
            CountNodeDescendantsUtil(node, ref count);

            return count;
        }

        private void CountNodeDescendantsUtil(TreeNode<T> node, ref int count)
        {
            if (node.Left != null)
            {
                count++;
                CountNodeDescendantsUtil(node.Left , ref count);
            }

            if (node.Right != null)
            {
                count++;
                CountNodeDescendantsUtil(node.Right , ref count);
            }
        }
    }
}
