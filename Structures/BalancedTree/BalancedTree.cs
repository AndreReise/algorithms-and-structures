using System;
using System.Collections.Generic;
using System.Linq;


namespace algorithms_and_structures.Structures.BalancedTree
{
    

    public partial class BalancedTree<T> : ITree<T> where T : IComparable
    {
        private bool _consistDuplicates;
        private List<T> _duplicatesList = new List<T>();

        public int Size { get; private set; }

        public TreeNode<T> Root;

        public BalancedTree()
        {
            Size = 0;
            _consistDuplicates = false;
            Root = null;
        }

        public T AddValues(T val1, T val2)
        {
            dynamic a = val1;
            dynamic b = val2;

            return a + b;
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

        public TreeNode<T> SearchNode(T value)
        {
            var node = this.Root;

            while (node != null)
            {
                if (node.Value.Equals(value))
                    return node;

                node = node.Value.CompareTo(value) == 1 ? node.Left : node.Right;
            }


            return null;
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

            var nodesList = this.GetPreOrderList();

            foreach (var x in nodesList)
            {
                newTree.Root = newTree.Insert(newTree.Root, x);
            }

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

        /// <summary>
        /// It count the number of left son nodes in a BBST
        /// </summary>
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
        }

        /// <summary>
        /// (). It finds the sum of keys in right son nodes in a BBST
        /// </summary>
        public T GetSumKeys(TreeNode<T> node)
        {
            T sum = default;
            GetSumKeysUtil(node , ref sum);

            return sum;
        }

        private void GetSumKeysUtil(TreeNode<T> node, ref T sum)
        {
            if (node.Right != null)
            {
                sum = AddValues(sum, node.Right.Value);
                GetSumKeysUtil(node.Right, ref sum);
            }
        }


        public bool EqualsToTree(BalancedTree<T> tree)
        {
            return EqualsToTreeUtils(this.Root, tree.Root);
        }

        private bool EqualsToTreeUtils(TreeNode<T> a, TreeNode<T> b)
        {
            if (a.Value.CompareTo(b.Value) != 0)
                return false;

            if (a.Right != null)
            {
                EqualsToTreeUtils(a.Right, b.Right);
            }

            if (a.Left != null)
            {
                EqualsToTreeUtils(a.Left, b.Left);
            }

            return true;
        }

        public TreeNode<T> FindSecondLargest()
        {
            var node = this.Root;

            if (this.Size < 2)
                throw new Exception("To few elements in collection");

            if (this.Root.Right == null)
                return this.Root.Left;


            while (node.Right.Right != null)
                node = node.Right;

            return this.Root.Value.CompareTo(node.Value) == 1 ? this.Root : node;
        }

        public BalancedTree<T> DeleteEvenElement()
        {
            if (typeof(T) != typeof(int))
                throw new Exception("Unsupported operation");

            var nodesList = new List<T>();
            ConvertToList(this.Root, ref nodesList);
            var toDelete = nodesList.Where( x => (int)Convert.ChangeType(x, typeof(int)) % 2 == 1);


            var newList = new BalancedTree<T>();

            foreach (var x in toDelete)
            {
                newList.Root = newList.Insert(newList.Root, x);
            }

            return newList;
        }

    }
}
